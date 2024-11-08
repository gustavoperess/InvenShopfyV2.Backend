using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Warehouse;

public interface IWarehouseHandler
{
    Task<Response<Models.Warehouse.Warehouse?>> CreateWarehouseAsync(CreateWarehouseRequest request);
    Task<Response<Models.Warehouse.Warehouse?>> UpdateWarehouseAsync(UpdateWarehouseRequest request);
    Task<Response<Models.Warehouse.Warehouse?>> DeleteWarehouseAsync(DeleteWarehouseRequest request);
    Task<Response<Models.Warehouse.Warehouse?>> GetWarehouseByIdAsync(GetWarehouseByIdRequest request);
    Task<Response<Models.Warehouse.Dto.WarehouseProductDto?>> GetTotalQuantityByWarehouseAndProductIdAsync(GetTotalQuantityByWarehouseAndProductIdRequest request);
    Task<Response<int?>> GetWarehouseQuantityAsync(GetWarehouseQuantityRequest request);
    Task<PagedResponse<List<Models.Warehouse.Warehouse>?>> GetWarehouseByPeriodAsync(GetAllWarehousesRequest request);
    
    Task<PagedResponse<List<Models.Warehouse.Dto.WarehouseName>?>> GetWarehouseNameAsync(GetAllWarehousesRequest request);

}