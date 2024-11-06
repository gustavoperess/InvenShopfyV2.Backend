using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Models.Warehouse;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Warehouses;

public class WarehouseHandler(AppDbContext context) : IWarehouseHandler
{
    public async Task<Response<Warehouse?>> CreateWarehouseAsync(CreateWarehouseRequest request)
    {
        try
        {
            var warehouse = new Warehouse
            {
                UserId = request.UserId,
                WarehouseName = request.WarehouseName,
                WarehousePhoneNumber = request.WarehousePhoneNumber,
                WarehouseEmail = request.WarehouseEmail,
                WarehouseCity = request.WarehouseCity,
                WarehouseCountry = request.WarehouseCountry,
                WarehouseZipCode = request.WarehouseZipCode,
                WarehouseOpeningNotes = request.WarehouseOpeningNotes
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

    public async Task<Response<Warehouse?>> UpdateWarehouseAsync(UpdateWarehouseRequest request)
    {
        try
        {
            var warehouse =
                await context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (warehouse is null)
            {
                return new Response<Warehouse?>(null, 404, "Warehouse not found");
            }

            warehouse.WarehouseName = request.WarehouseName;
            warehouse.WarehousePhoneNumber = request.WarehousePhoneNumber;
            warehouse.WarehouseEmail = request.WarehouseEmail;
            warehouse.WarehouseCity = request.WarehouseCity;
            warehouse.WarehouseZipCode = request.WarehouseZipCode;
            warehouse.WarehouseCountry = request.WarehouseCountry;
            warehouse.WarehouseOpeningNotes = request.WarehouseOpeningNotes;
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync();
            return new Response<Warehouse?>(warehouse, message: "Warehouse updated successfully");
        }
        catch
        {
            return new Response<Warehouse?>(null, 500, "It was not possible to update this warehouse");
        }
    }

    public async Task<Response<Warehouse?>> DeleteWarehouseAsync(DeleteWarehouseRequest request)
    {
        try
        {
            var warehouse =
                await context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

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

    public async Task<Response<Warehouse?>> GetWarehouseByIdAsync(GetWarehouseByIdRequest byIdRequest)
    {
        try
        {
            var warehouse =
                await context.Warehouses.FirstOrDefaultAsync(x =>
                    x.Id == byIdRequest.Id && x.UserId == byIdRequest.UserId);

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

    public async Task<PagedResponse<List<Warehouse>?>> GetWarehouseByPeriodAsync(GetAllWarehousesRequest request)
    {
        try
        {
            var query = context
                .Warehouses
                .AsNoTracking()
                .GroupJoin(context.Purchases,
                    warehouse => warehouse.Id,
                    purchase => purchase.WarehouseId,
                    (warehouse, purchases) => new { warehouse, purchases })
                .Where(x => x.warehouse.UserId == request.UserId || x.purchases.Any(p => p.UserId == request.UserId))
                .Select(g => new
                {
                    g.warehouse.Id,
                    g.warehouse.WarehousePhoneNumber,
                    g.warehouse.WarehouseEmail,
                    g.warehouse.WarehouseCity,
                    g.warehouse.WarehouseName,
                    g.warehouse.WarehouseCountry,
                    g.warehouse.WarehouseOpeningNotes,
                    g.warehouse.WarehouseZipCode,
                    TotalQuantityItems = g.purchases.Sum(p => p.TotalNumberOfProductsBought)
                })
                .OrderBy(x => x.WarehouseCity);

         
            var totalCount = await query.CountAsync();

        
            var warehouses = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

          
            var result = warehouses.Select(w => new Warehouse
            {
                Id = w.Id,
                WarehouseEmail = w.WarehouseEmail,
                WarehouseCity = w.WarehouseCity,
                WarehouseCountry = w.WarehouseCountry,
                WarehouseZipCode = w.WarehouseZipCode,
                WarehouseOpeningNotes = w.WarehouseOpeningNotes,
                WarehousePhoneNumber = w.WarehousePhoneNumber,
                WarehouseName = w.WarehouseName,
                QuantityOfItems = w.TotalQuantityItems, 
            }).ToList();

            return new PagedResponse<List<Warehouse>?>(
                result,
                totalCount,
                request.PageNumber,
                request.PageSize);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<Warehouse>?>(null, 500, $"An error occurred: {ex.Message}");
        }
    }

    public async Task<Response<int?>> GetWarehouseQuantityAsync(GetWarehouseQuantityRequest request)
    {
        try
        {
            var totalSalesAmount = await context.Warehouses.CountAsync();
            return new Response<int?>(totalSalesAmount, 200, "Total number of warehouses retrieved successfully");
        }
        catch
        {
            return new Response<int?>(null, 500, "It was not possible to consult the total number of warehouses");
        }
    }
}