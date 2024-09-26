using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Products;

public class ProductHandler(AppDbContext context) : IProductHandler
{
    public async Task<Response<Product?>> CreateAsync(CreateProductRequest request)
    {
        try
        {
            var product = new Product
            {
                UserId = request.UserId,
                Title = request.Title,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                ProductCode = request.ProductCode,
                CreateAt = DateTime.Now,
                UnitId = request.UnitId,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                Subcategory = request.Subcategory,
                ProductImage = request.ProductImage,
                Featured = request.Featured,
                DifferPriceWarehouse = request.DifferPriceWarehouse,
                Expired = request.Expired,
                Sale = request.Sale

            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
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
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (product is null)
            {
                return new Response<Product?>(null, 404, "Product not found");
            }
            
            product.Title = request.Title;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.ProductCode = request.ProductCode;
            product.UnitId = request.UnitId;
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            product.ProductImage = request.ProductImage;
            context.Products.Update(product);
            await context.SaveChangesAsync();
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
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (product is null)
            {
                return new Response<Product?>(null, 404, "Product not found");
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
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
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
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
    public async Task<PagedResponse<List<Product>?>> GetByPeriodAsync(GetAllProductsRequest request)
    {
        try
        {
            var query = context
                .Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Unit)
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Featured);
            
            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Product>?>(
                products,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Product>?>(null, 500, "It was not possible to consult all Products");
        }
    }

    public async Task<PagedResponse<List<Product>?>> GetByPartialNameAsync(GetProductByNameRequest request)
    {
        try
        {
            var products = await context.Products
                .Where(x => EF.Functions.ILike(x.Title, $"%{request.Title}%") && x.UserId == request.UserId)
                .ToListAsync();
            
            if (products == null || products.Count == 0)
            {
                return new PagedResponse<List<Product>?>(null, 404, "Product not found");
            }
            return new PagedResponse<List<Product>?>(products);
    
        }
        catch
        {
            return new PagedResponse<List<Product>?>(null, 500, "It was not possible to consult all Products");
        }
    }
}