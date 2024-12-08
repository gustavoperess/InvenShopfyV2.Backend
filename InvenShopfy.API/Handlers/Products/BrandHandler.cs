using System.Globalization;
using InvenShopfy.API.Common.CloudinaryServiceNamespace;
using InvenShopfy.API.Data;
using InvenShopfy.Core;
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
    
    public async Task<Response<Brand?>> CreateProductBrandAsync(CreateBrandRequest request)
    {
        try
        {
            // if (!request.UserHasPermission)
            // {
            //     return new Response<Brand?>(null, 409, $"{Configuration.NotAuthorized} 'create'");
            // }

            
            var unit = await _context.Unit.FirstOrDefaultAsync(x => x.UnitName.ToLower() == request.BrandName.ToLower());
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            if (unit != null)
            {
                return new Response<Brand?>(null, 409, $"A Brand with the name '{request.BrandName}' already exists.");
            }
            
            var brand = new Brand
            { 
                BrandName = textInfo.ToTitleCase(request.BrandName),
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

            return new Response<Brand?>(brand, 201, $"Brand {Configuration.Created}");

        }
        catch
        {
            return new Response<Brand?>(null, 500, $"{Configuration.NotCreated} Brand");
        }
    }

    public async Task<Response<Brand?>> UpdateProductBrandAsync(UpdateBrandRequest request)
    {
        try
        {
            
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (brand is null)
            {
                return new Response<Brand?>(null, 404, $"Brand {Configuration.NotFound}");
            }
            
            brand.BrandName = request.BrandName;
            brand.BrandImage = request.BrandImage;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            return new Response<Brand?>(brand, message: $"Brand {Configuration.Updated}");

        }
        catch 
        {
            return new Response<Brand?>(null, 500, $"{Configuration.NotUpdated} Brand");
        }
    }

    public async Task<Response<Brand?>> DeleteProductBrandAsync(DeleteBrandRequest request)
    {
        try
        {
            // if (!request.UserHasPermission)
            // {
            //     return new Response<Brand?>(null, 400, $"{Configuration.NotAuthorized} 'Delete'");
            // }
            
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (brand is null)
            {
                return new Response<Brand?>(null, 404, $"Brand {Configuration.NotFound}");
            }
            
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return new Response<Brand?>(brand, message: $"Brand {Configuration.Deleted}");

        }
        catch 
        {
            return new Response<Brand?>(null, 500, $"{Configuration.NotDeleted} Brand");
        }
    }
    
    public async Task<Response<Brand?>> GetProductBrandByIdAsync(GetBrandByIdRequest request)
    {
        try
        {
            var brand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (brand is null)
            {
                return new Response<Brand?>(null, 404, $"Brand {Configuration.NotFound}");
            }

            return new Response<Brand?>(brand);

        }
        catch 
        {
            return new Response<Brand?>(null, 500, $"{Configuration.NotRetrived} Brand");
        }
    }
    public async Task<PagedResponse<List<Brand>?>> GetProductBrandByPeriodAsync(GetAllBrandsRequest request)
    {
        try
        {
            // if (!request.UserHasPermission)
            // {
            //     return new PagedResponse<List<Brand>?>([], 201, $"{Configuration.NotAuthorized}");
            // }
            
            var query = _context
                .Brands
                .AsNoTracking()
                .OrderBy(x => x.BrandName);
            
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
            return new PagedResponse<List<Brand>?>(null, 500, $"{Configuration.NotRetrived} all brands ");
        }
    }
}