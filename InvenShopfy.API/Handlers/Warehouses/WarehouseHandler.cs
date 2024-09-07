using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Models.Warehouse;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Warehouses;

public class WarehouseHandler (AppDbContext context) : IWarehouseHandler
{
    public async Task<Response<Warehouse?>> CreateAsync(CreateWarehouseRequest request)
    {
        try
        {
            var warehouse = new Warehouse
            {
                UserId = request.UserId,
                WarehouseName = request.WarehouseName,
                PhoneNumber = request.UserId,
                Email = request.UserId,
                Address = request.Address,
                ZipCode = request.ZipCode,
            };
            await context.Warehouses.AddAsync(warehouse);
            await context.SaveChangesAsync();

            return new Response<Warehouse?>(warehouse, 201, "warehouse created successfully");

        }
        catch
        {
            return new Response<Warehouse?>(null, 500, "It was not possible to create a new warehouse");
        }
    }

    public async Task<Response<Warehouse?>> UpdateAsync(UpdateWarehouseRequest request)
    {
        try
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (warehouse is null)
            {
                return new Response<Warehouse?>(null, 404, "Warehouse not found");
            }

            warehouse.WarehouseName = request.WarehouseName;
            warehouse.PhoneNumber = request.UserId;
            warehouse.Email = request.UserId;
            warehouse.Address = request.Address;
            warehouse.ZipCode = request.ZipCode;
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync();
            return new Response<Warehouse?>(warehouse, message: "Warehouse updated successfully");

        }
        catch 
        {
            return new Response<Warehouse?>(null, 500, "It was not possible to update this warehouse");
        }
    }

    public async Task<Response<Warehouse?>> DeleteAsync(DeleteWarehouseRequest request)
    {
        try
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (warehouse is null)
            {
                return new Response<Warehouse?>(null, 404, "Warehouse not found");
            }

            context.Warehouses.Remove(warehouse);
            await context.SaveChangesAsync();
            return new Response<Warehouse?>(warehouse, message: "warehouse removed successfully");

        }
        catch 
        {
            return new Response<Warehouse?>(null, 500, "It was not possible to delete this warehouse");
        }
    }
    
    public async Task<Response<Warehouse?>> GetByIdAsync(GetWarehouseByIdRequest byIdRequest)
    {
        try
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(x => x.Id == byIdRequest.Id && x.UserId == byIdRequest.UserId);
            
            if (warehouse is null)
            {
                return new Response<Warehouse?>(null, 404, "warehouse not found");
            }

            return new Response<Warehouse?>(warehouse);

        }
        catch 
        {
            return new Response<Warehouse?>(null, 500, "It was not possible to find this warehouse");
        }
    }
    public async Task<PagedResponse<List<Warehouse>?>> GetByPeriodAsync(GetAllWarehousesRequest request)
    {
        try
        {
            var query = context
                .Warehouses
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.WarehouseName);
            
            var warehouse = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Warehouse>?>(
                warehouse,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Warehouse>?>(null, 500, "It was not possible to consult all warehouses");
        }
    }
}