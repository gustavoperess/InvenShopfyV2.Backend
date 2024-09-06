using InvenShopfy.Core.Requests.Brand;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Warehouse;

public interface IWarehouseHandler
{
    Task<Response<Models.Warehouse.Warehouse?>> CreateAsync(CreateWarehouseRequest request);
    Task<Response<Models.Warehouse.Warehouse?>> UpdateAsync(UpdateWarehouseRequest request);
    Task<Response<Models.Warehouse.Warehouse?>> DeleteAsync(DeleteWarehouseRequest request);
    Task<Response<Models.Warehouse.Warehouse?>> GetByIdAsync(GetWarehouseByIdRequest request);
    Task<PagedResponse<List<Models.Warehouse.Warehouse>?>> GetByPeriodAsync(GetAllWarehousesRequest request);
}