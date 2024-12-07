using System.Globalization;
using InvenShopfy.API.Data;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Notifications;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Models.Warehouse;
using InvenShopfy.Core.Models.Warehouse.Dto;
using InvenShopfy.Core.Requests.Notifications;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Warehouses;

public class WarehouseHandler : IWarehouseHandler
{
    private readonly AppDbContext _context;
    private readonly INotificationHandler _notificationHandler; 

    public WarehouseHandler(AppDbContext context, INotificationHandler notificationHandler)
    {
        _context = context;
        _notificationHandler = notificationHandler;
    }
    public async Task<Response<Warehouse?>> CreateWarehouseAsync(CreateWarehouseRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new Response<Warehouse?>(null, 409, $"{Configuration.NotAuthorized} 'create'");
            }
            
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var existingWarehouse = await _context.Warehouses
                .FirstOrDefaultAsync(
                    x => x.WarehouseName.ToLower() == request.WarehouseName.ToLower() || x.WarehouseEmail.ToLower() == request.WarehouseEmail.ToLower());
            
            if (existingWarehouse != null)
            {
                if (existingWarehouse.WarehouseName.ToLower() == request.WarehouseName.ToLower())
                {
                    return new Response<Warehouse?>(null, 409, $"A Warehouse with the name'{request.WarehouseName}' already exists.");
                }
            
                if (existingWarehouse.WarehouseEmail.ToLower() == request.WarehouseEmail.ToLower())
                {
                    return new Response<Warehouse?>(null, 409, $"A Warehouse with email '{request.WarehouseEmail}' already exists.");
                }
            }
            var warehouse = new Warehouse
            {
                UserId = request.UserId,
                WarehouseName = textInfo.ToTitleCase(request.WarehouseName),
                WarehousePhoneNumber = request.WarehousePhoneNumber,
                WarehouseEmail = request.WarehouseEmail,
                WarehouseCity = textInfo.ToTitleCase(request.WarehouseCity),
                WarehouseCountry = textInfo.ToTitleCase(request.WarehouseCountry),
                WarehouseZipCode = request.WarehouseZipCode,
                WarehouseOpeningNotes = request.WarehouseOpeningNotes
              
            };
            
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            
            var notificationRequest = new CreateNotificationsRequest
            {
                NotificationTitle =  $"New Warehouse : {request.WarehouseName} created",
                Urgency = true,
                From = "System-Warehouse", 
                Image = null, 
                Href = "/warehouse/warehouselist",
            };
            await _notificationHandler.CreateNotificationAsync(notificationRequest);
            return new Response<Warehouse?>(null, 201, "warehouse created successfully");
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
                await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id);

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
            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
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
            if (!request.UserHasPermission)
            {
                return new Response<Warehouse?>(null, 400, $"{Configuration.NotAuthorized} 'Delete'");
            }
            
            var warehouse =
                await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (warehouse is null)
            {
                return new Response<Warehouse?>(null, 404, "Warehouse not found");
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
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
                await _context.Warehouses.AsNoTracking().FirstOrDefaultAsync(x =>
                    x.Id == byIdRequest.Id);

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
    
    
    public async Task<Response<WarehouseProductDto?>> GetTotalQuantityByWarehouseAndProductIdAsync(GetTotalQuantityByWarehouseAndProductIdRequest request)
    {
        try
        {
            
            var response = await _context.WarehousesProducts
                .AsNoTracking()
                .Where(x =>
                    x.WarehouseId == request.WarehouseId &&
                    x.ProductId == request.ProductId)
                .Select(x => new WarehouseProductDto
                {
                    ProductId = x.ProductId,
                    WarehouseId = x.WarehouseId,
                    Quantity = x.Quantity
                })
                .FirstOrDefaultAsync();
            
            if (response?.Quantity <= 0 || response?.Quantity == null)
            {
                return new Response<WarehouseProductDto?>(new WarehouseProductDto
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.WarehouseId,
                    Quantity = 0 
                }, 200, "Warehouse does not contain this item");
            }
            
            if (response?.WarehouseId is null)
            {
                return new Response<WarehouseProductDto?>(null, 404, "Warehouse Id does not exist");
            }
           
          
            return new Response<WarehouseProductDto?>(response);
        }
        catch
        {
            return new Response<WarehouseProductDto?>(null, 500, "It was not possible to find the total quantity");
        }
    }
    
    public async Task<PagedResponse<List<Warehouse>?>> GetWarehouseByPeriodAsync(GetAllWarehousesRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new PagedResponse<List<Warehouse>?>([], 201, $"{Configuration.NotAuthorized}");
            }
            
            var query = _context
                .Warehouses
                .AsNoTracking()
                .GroupJoin(_context.WarehousesProducts,
                    warehouse => warehouse.Id,
                    warehouseProduct => warehouseProduct.WarehouseId,
                    (warehouse, warehouseProduct) => new { warehouse, warehouseProduct })
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
                    TotalQuantityItems = g.warehouseProduct.Sum(p => p.Quantity)
                })
                .OrderBy(x => x.Id);

         
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

    public async Task<Response<int?>> GetWarehouseQuantityAsync()
    {
        try
        {
            var totalSalesAmount = await _context.Warehouses.AsNoTracking().CountAsync();
            return new Response<int?>(totalSalesAmount, 200, "Total number of warehouses retrieved successfully");
        }
        catch
        {
            return new Response<int?>(null, 500, "It was not possible to consult the total number of warehouses");
        }
    }

    public async Task<Response<int?>> GetTotalInStockAsync()
    {
        try
        {
            var stockQuantity = await _context.WarehousesProducts.AsNoTracking().SumAsync(x => x.Quantity);
            return new Response<int?>(stockQuantity, 200, "Total stock quantity retrieved successfully"); 
        }
        catch
        {
            return new Response<int?>(0, 400, "It was not possible to retrive the total stock quantity"); 
        }
    }
    
    public async Task<PagedResponse<List<WarehouseName>?>> GetWarehouseNameAsync(GetAllWarehousesRequest request)
    {
        try
        {
            var query = _context
                .Warehouses
                .AsNoTracking()
                .Select(x => new WarehouseName
                { 
                    Id = x.Id,
                    WarehouseTitle = x.WarehouseName
                    
                })
                .OrderBy(x => x.WarehouseTitle);
            
            var warehouse = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<WarehouseName>?>(
                warehouse,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<WarehouseName>?>(null, 500, "It was not possible to consult all brands");
        }
    }
}