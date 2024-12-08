using System.Globalization;
using InvenShopfy.API.Common.CloudinaryServiceNamespace;
using InvenShopfy.API.Data;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Notifications;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Models.Product.Dto;
using InvenShopfy.Core.Requests.Notifications;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Products;

public class ProductHandler : IProductHandler
{
    
    private readonly AppDbContext _context;
    private readonly CloudinaryService _cloudinaryService;
    private readonly INotificationHandler _notificationHandler;
    public ProductHandler(
        AppDbContext context, 
        CloudinaryService cloudinaryService, 
        INotificationHandler notificationHandler) 
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
        _notificationHandler = notificationHandler;
    }
  
    public async Task<Response<Product?>> CreateProductAsync(CreateProductRequest request)
    {
        try
        {
            // if (!request.UserHasPermission)
            // {
            //     return new Response<Product?>(null, 409, $"{Configuration.NotAuthorized} 'create'");
            // }

            var findByName = await _context.Products.FirstOrDefaultAsync(x => x.ProductName.ToLower() == request.ProductName.ToLower());
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            if (findByName != null)
            {
                return new Response<Product?>(null, 409, $"A product with the name '{request.ProductName}' already exists.");
            }
            
            var product = new Product
            {
              
                ProductName = textInfo.ToTitleCase(request.ProductName),
                ProductPrice = request.ProductPrice,
                ProductCode = request.ProductCode,
                CreateAt = DateOnly.FromDateTime(DateTime.Now),
                UnitId = request.UnitId,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                SubCategory = request.Subcategory,
                Featured = request.Featured,
                TaxPercentage = request.TaxPercentage,
                MarginRange = request.MarginRange,
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

            var notificationRequest = new CreateNotificationsRequest
            {
                NotificationTitle =  $"New product created: {product.ProductName}",
                Urgency = false,
                From = "System-Products", 
                Image = product.ProductImage,
                Href = "/product/productlist",
            };
             await _notificationHandler.CreateNotificationAsync(notificationRequest);
            
            return new Response<Product?>(product, 201, "Product created successfully");
        }
        catch
        {
            
            return new Response<Product?>(null, 500, "It was not possible to create a new Product");
        }
    }
    
    public async Task<Response<ProductByNameForUpdatePage?>> UpdateProductAsync(UpdateProductRequest request)
    {
        try
        {
            var product = await _context.Products
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();
            if (product is null)
            {
                return new Response<ProductByNameForUpdatePage?>(null, 404, "Product not found");
            }
            
            if(request.ProductName != null)
            {
                var existingProductWithName = await _context.Products
                    .Where(x => x.ProductName.ToLower() == request.ProductName.ToLower() && x.Id != request.Id)
                    .FirstOrDefaultAsync();
            
                if (existingProductWithName != null)
                {
                    return new Response<ProductByNameForUpdatePage?>(null, 409, $"A product with the name '{request.ProductName}' already exists.");
                }
                
                product.ProductName = request.ProductName;
            }

            if (request.ProductPrice != null)
            {
           
            }
            product.ProductPrice = request.ProductPrice ?? product.ProductPrice;
            product.UnitId = request.UnitId ?? product.UnitId; 
            product.BrandId = request.BrandId ?? product.BrandId; 
            product.CategoryId = request.CategoryId ?? product.CategoryId;
            product.TaxPercentage = request.TaxPercentage ?? product.TaxPercentage; 
            product.MarginRange = request.MarginRange ?? product.MarginRange; 
            product.SubCategory = request.SubCategory ?? product.SubCategory;
            
            if (request.ProductImage != null)
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(request.ProductImage, "invenShopfy/Products");
                product.ProductImage = uploadResult.SecureUrl.ToString(); 
            }
            
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return new Response<ProductByNameForUpdatePage?>(null, message: "Product updated successfully");

        }
        catch 
        {
            return new Response<ProductByNameForUpdatePage?>(null, 500, "It was not possible to update this Product");
        }
    }

    public async Task<Response<Product?>> DeleteProductAsync(DeleteProductRequest request)
    {
        try
        {
            
            // if (!request.UserHasPermission)
            // {
            //     return new Response<Product?>(null, 400, $"{Configuration.NotAuthorized} 'Delete'");
            // }

            
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            
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
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
            
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
            // if (!request.UserHasPermission)
            // {
            //     return new PagedResponse<List<ProductList>?>([], 201, $"{Configuration.NotAuthorized}");
            // }
            
            
            var query = _context
                .Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Unit)
                .Select(g => new
                {
                    g.Id,
                    g.ProductName,
                    g.ProductPrice,
                    g.StockQuantity,
                    g.ProductImage,
                    g.ProductCode,
                    g.TaxPercentage,
                    g.MarginRange,
                    g.Unit.UnitName,
                    g.Brand.BrandName,
                    CategoryName = g.Category.MainCategory,
                    SubCategories = g.SubCategory,
                    
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
                ProductName = s.ProductName,
                ProductPrice = s.ProductPrice,
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
                .Where(x => EF.Functions.ILike(x.ProductName, $"%{request.ProductName}%"))
                .Select(g => new ProductByName
                {
                    Id = g.Id,
                    ProductName = g.ProductName,
                    ProductPrice = g.ProductPrice,
                    StockQuantity = g.StockQuantity,
                    ProductImage = g.ProductImage,
                    ProductCode = g.ProductCode,
                    MarginRange = g.MarginRange,
                    TaxPercentage = g.TaxPercentage,
                    Category = g.Category.MainCategory,
                    Subcategory = g.SubCategory,
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
    
    
    public async Task<PagedResponse<List<ProductByNameForUpdatePage>?>> GetProductByPartialNameForUpdatePageAsync(GetProductByNameRequest request)
    {
        try
        {
            var products = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(x => EF.Functions.ILike(x.ProductName, $"%{request.ProductName}%"))
                .Select(g => new ProductByNameForUpdatePage
                {
                    Id = g.Id,
                    ProductName = g.ProductName,
                    ProductPrice = g.ProductPrice,
                    ProductImage = g.ProductImage,
                    ProductCode = g.ProductCode,
                    MarginRange = g.MarginRange,
                    TaxPercentage = g.TaxPercentage,
                    MainCategoryId = g.Category.Id,
                    BrandName = g.Brand.BrandName,
                    UnitName = g.Unit.UnitName,
                    Expired = g.Expired
                    
                }).ToListAsync();
            
            if (products.Count == 0)
            {
                return new PagedResponse<List<ProductByNameForUpdatePage>?>(new List<ProductByNameForUpdatePage>(), 200, "No products found");
            }
            
            return new PagedResponse<List<ProductByNameForUpdatePage>?>(products);
    
        }
        catch
        {
            return new PagedResponse<List<ProductByNameForUpdatePage>?>(null, 500, "It was not possible to consult all Products");
        }
    }
}