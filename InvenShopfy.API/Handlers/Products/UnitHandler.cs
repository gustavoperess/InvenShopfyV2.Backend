using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Unit;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Products;

public class UnitHandler(AppDbContext context) : IUnitHandler
{
    public async Task<Response<Unit?>> CreateProductUnitAsync(CreateUnitRequest request)
    {   
        try
        {
            var unit = new Unit
            {
                UserId = request.UserId,
                Title = request.Title,
                ShortName = request.ShortName
            };

            var unitName = context.Unit.FirstOrDefault(
                x => x.Title.ToLower() == request.Title.ToLower() || 
                     x.ShortName.ToLower() == request.ShortName.ToLower());
            if (unitName != null)
            {
                return new Response<Unit?>(null, 500, "This Unit/short Name already exist");
            } 
            
            
            await context.Unit.AddAsync(unit);
            await context.SaveChangesAsync();

            return new Response<Unit?>(unit, 201, "Unit created successfully");

        }
        catch
        {
            return new Response<Unit?>(null, 500, "It was not possible to create a new Unit");
        }
    }

    public async Task<Response<Unit?>> UpdateProductUnitAsync(UpdateUnitRequest request)
    {
        try
        {
            var unit = await context.Unit.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (unit is null)
            {
                return new Response<Unit?>(null, 404, "Unit not found");
            }
            
            unit.Title = request.Title;
            unit.ShortName = request.ShortName;
            context.Unit.Update(unit);
            await context.SaveChangesAsync();
            return new Response<Unit?>(unit, message: "Unit updated successfully");

        }
        catch 
        {
            return new Response<Unit?>(null, 500, "It was not possible to update this Unit");
        }
    }

    public async Task<Response<Unit?>> DeleteProductUnitAsync(DeleteUnitRequest request)
    {
        try
        {
            var unit = await context.Unit.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (unit is null)
            {
                return new Response<Unit?>(null, 404, "Unit not found");
            }

            context.Unit.Remove(unit);
            await context.SaveChangesAsync();
            return new Response<Unit?>(unit, message: "unit removed successfully");

        }
        catch 
        {
            return new Response<Unit?>(null, 500, "It was not possible to delete this Unit");
        }
    }
    
    public async Task<Response<Unit?>> GetProductUnitByIdAsync(GetUnitByIdRequest request)
    {
        try
        {
            var unit = await context.Unit.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (unit is null)
            {
                return new Response<Unit?>(null, 404, "Unit not found");
            }

            return new Response<Unit?>(unit);

        }
        catch 
        {
            return new Response<Unit?>(null, 500, "It was not possible to find this Unit");
        }
    }
    public async Task<PagedResponse<List<Unit>?>> GetProductUnitByPeriodAsync(GetAllUnitRequest request)
    {
        try
        {
            var query = context
                .Unit
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);
            
            var units = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Unit>?>(
                units,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Unit>?>(null, 500, "It was not possible to consult all units");
        }
    }
}