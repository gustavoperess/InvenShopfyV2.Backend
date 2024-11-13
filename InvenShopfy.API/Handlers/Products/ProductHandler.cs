using InvenShopfy.API.Common.CloudinaryServiceNamespace;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Models.Product.Dto;
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
  
    public async Task<Response<Product?>> CreateProductAsync(CreateProductRequest request)
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
                TaxPercentage = request.TaxPercentage,
                MarginRange = request.MarginRange,
                DifferPriceWarehouse = request.DifferPriceWarehouse,
                Expired = request.Expired,
                Sale = request.Sale
        
            };
            if (request.ProductImage == null)
            {
                product.ProductImage = "https://res.cloudinary.com/dououppib/image/upload/v1729977408/InvenShopfy/Products/mfbbhovoccem7sxsberb.png";
            }
            else
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(request.ProductImage, "invenShopfy/Products");
                product.ProductImage = uploadResult.SecureUrl.ToString(); 
            }
            
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return new Response<Product?>(product, 201, "Product created successfully");
        }
        catch
        {
            
            return new Response<Product?>(null, 500, "It was not possible to create a new Product");
        }
    }
    
    public async Task<Response<Product?>> UpdateProductAsync(UpdateProductRequest request)
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

    public async Task<Response<Product?>> DeleteProductAsync(DeleteProductRequest request)
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
    
    public async Task<Response<Product?>> GetProductByIdAsync(GetProductByIdRequest request)
    {
        try
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
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
    public async Task<PagedResponse<List<ProductList>?>> GetProductByPeriodAsync(GetAllProductsRequest request)
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
                    g.Id,
                    g.Title,
                    g.Price,
                    g.StockQuantity,
                    g.ProductImage,
                    g.ProductCode,
                    g.TaxPercentage,
                    g.MarginRange,
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
            
            var result = products.Select(s => new ProductList
            {
                Id = s.Id,
                Title = s.Title,
                Price = s.Price,
                ProductImage = s.ProductImage,
                StockQuantity = s.StockQuantity,
                UnitName = s.UnitName,
                MarginRange = s.MarginRange,
                TaxPercentage = s.TaxPercentage,
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

    public async Task<PagedResponse<List<ProductByName>?>> GetProductByPartialNameAsync(GetProductByNameRequest request)
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
                    MarginRange = g.MarginRange,
                    TaxPercentage = g.TaxPercentage,
                    Category = g.Category.MainCategory,
                    Subcategory = g.Subcategory,
                    UserId = g.UserId,
                    Expired = g.Expired
                    
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