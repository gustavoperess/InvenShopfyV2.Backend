using InvenShopfy.API.Common.CloudinaryServiceNamespace;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Products;

public class ProductHandler : IProductHandler
{
    
    private readonly AppDbContext _context;
    private readonly CloudinaryService _cloudinaryService;
    public ProductHandler(AppDbContext context, CloudinaryService cloudinaryService) 
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }
  
    public async Task<Response<Product?>> CreateAsync(CreateProductRequest request)
    {
   
        try
        {
            var product = new Product
            {
                UserId = request.UserId,
                Title = request.Title,
                Price = request.Price,
                ProductCode = request.ProductCode,
                CreateAt = DateOnly.FromDateTime(DateTime.Now),
                UnitId = request.UnitId,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                Subcategory = request.Subcategory,
                Featured = request.Featured,
                DifferPriceWarehouse = request.DifferPriceWarehouse,
                Expired = request.Expired,
                Sale = request.Sale
        
            };
            var uploadResult = await _cloudinaryService.UploadImageAsync(request.ProductImage, "invenShopfy/Products");
            product.ProductImage = uploadResult.SecureUrl.ToString(); 
            
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return new Response<Product?>(product, 201, "Product created successfully");
        }
        catch
        {
            return new Response<Product?>(null, 500, "It was not possible to create a new Product");
        }
    }
    
    public async Task<Response<Product?>> UpdateAsync(UpdateProductRequest request)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (product is null)
            {
                return new Response<Product?>(null, 404, "Product not found");
            }
            
            product.Title = request.Title;
            product.Price = request.Price;
            product.ProductCode = request.ProductCode;
            product.UnitId = request.UnitId;
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            product.ProductImage = request.ProductImage;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return new Response<Product?>(product, message: "Product updated successfully");

        }
        catch 
        {
            return new Response<Product?>(null, 500, "It was not possible to update this Product");
        }
    }

    public async Task<Response<Product?>> DeleteAsync(DeleteProductRequest request)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (product is null)
            {
                return new Response<Product?>(null, 404, "Product not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new Response<Product?>(product, message: "Product removed successfully");

        }
        catch 
        {
            return new Response<Product?>(null, 500, "It was not possible to delete this Product");
        }
    }
    
    public async Task<Response<Product?>> GetByIdAsync(GetProductByIdRequest request)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (product is null)
            {
                return new Response<Product?>(null, 404, "Product not found");
            }
            return new Response<Product?>(product);

        }
        catch 
        {
            return new Response<Product?>(null, 500, "It was not possible to find this Product");
        }
    }
    public async Task<PagedResponse<List<ProductList>?>> GetByPeriodAsync(GetAllProductsRequest request)
    {
        try
        {
            var query = _context
                .Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Unit)
                .Where(x => x.UserId == request.UserId)
                .Select(g => new
                {
                    Id = g.Id,
                    Title = g.Title,
                    Price = g.Price,
                    StockQuantity = g.StockQuantity,
                    ProductImage = g.ProductImage,
                    ProductCode = g.ProductCode,
                    UnitName = g.Unit.Title,
                    BrandName = g.Brand.Title,
                    CategoryName = g.Category.MainCategory,
                    SubCategories = g.Subcategory,
                    
                })
                .OrderBy(x => x.StockQuantity);
            
            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            var result = products.Select(s => new Core.Models.Product.ProductList
            {
                Id = s.Id,
                Title = s.Title,
                Price = s.Price,
                ProductImage = s.ProductImage,
                StockQuantity = s.StockQuantity,
                UnitName = s.UnitName,
                BrandName = s.BrandName,
                ProductCode = s.ProductCode,
                CategoryName = s.CategoryName,
                SubCategories = s.SubCategories,
               
            }).ToList();
            
            return new PagedResponse<List<ProductList>?>(
                result,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<ProductList>?>(null, 500, "It was not possible to consult all Products");
        }
    }

    public async Task<PagedResponse<List<ProductByName>?>> GetByPartialNameAsync(GetProductByNameRequest request)
    {
        try
        {
            var products = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(x => EF.Functions.ILike(x.Title, $"%{request.Title}%") && x.UserId == request.UserId)
                .Select(g => new ProductByName
                {
                    Id = g.Id,
                    Title = g.Title,
                    Price = g.Price,
                    StockQuantity = g.StockQuantity,
                    ProductImage = g.ProductImage,
                    ProductCode = g.ProductCode,
                    Category = g.Category.MainCategory,
                    Subcategory = g.Subcategory,
                    UserId = g.UserId
                    
                }).OrderBy(x => x.StockQuantity)
                .ToListAsync();
            
            if (products.Count == 0)
            {
                return new PagedResponse<List<ProductByName>?>(new List<ProductByName>(), 200, "No products found");
            }
            
            return new PagedResponse<List<ProductByName>?>(products);
    
        }
        catch
        {
            return new PagedResponse<List<ProductByName>?>(null, 500, "It was not possible to consult all Products");
        }
    }
}