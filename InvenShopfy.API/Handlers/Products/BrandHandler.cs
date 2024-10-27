using InvenShopfy.API.Common.CloudinaryServiceNamespace;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Products;

public class BrandHandler : IBrandHandler
{
    private readonly AppDbContext _context;
    private readonly CloudinaryService _cloudinaryService;
    public BrandHandler(AppDbContext context, CloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }
    
    public async Task<Response<Brand?>> CreateAsync(CreateBrandRequest request)
    {
        try
        {
            var brand = new Brand
            {
                UserId = request.UserId,
                Title = request.Title,
            };
            if (request.BrandImage == null)
            {
                brand.BrandImage = "https://res.cloudinary.com/dououppib/image/upload/v1729977408/InvenShopfy/Products/mfbbhovoccem7sxsberb.png";
            }
            else
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(request.BrandImage, "invenShopfy/Brands");
                brand.BrandImage = uploadResult.SecureUrl.ToString();
            }
            
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return new Response<Brand?>(brand, 201, "Brand created successfully");

        }
        catch
        {
            return new Response<Brand?>(null, 500, "It was not possible to create a new Brand");
        }
    }

    public async Task<Response<Brand?>> UpdateAsync(UpdateBrandRequest request)
    {
        try
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (brand is null)
            {
                return new Response<Brand?>(null, 404, "Brand not found");
            }
            
            brand.Title = request.Title;
            brand.BrandImage = request.BrandImage;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            return new Response<Brand?>(brand, message: "brand updated successfully");

        }
        catch 
        {
            return new Response<Brand?>(null, 500, "It was not possible to update this Brand");
        }
    }

    public async Task<Response<Brand?>> DeleteAsync(DeleteBrandRequest request)
    {
        try
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (brand is null)
            {
                return new Response<Brand?>(null, 404, "Brand not found");
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return new Response<Brand?>(brand, message: "brand removed successfully");

        }
        catch 
        {
            return new Response<Brand?>(null, 500, "It was not possible to delete this Brand");
        }
    }
    
    public async Task<Response<Brand?>> GetByIdAsync(GetBrandByIdRequest request)
    {
        try
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (brand is null)
            {
                return new Response<Brand?>(null, 404, "Brand not found");
            }

            return new Response<Brand?>(brand);

        }
        catch 
        {
            return new Response<Brand?>(null, 500, "It was not possible to find this Brand");
        }
    }
    public async Task<PagedResponse<List<Brand>?>> GetByPeriodAsync(GetAllBrandsRequest request)
    {
        try
        {
            var query = _context
                .Brands
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);
            
            var brands = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Brand>?>(
                brands,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Brand>?>(null, 500, "It was not possible to consult all brands");
        }
    }
}