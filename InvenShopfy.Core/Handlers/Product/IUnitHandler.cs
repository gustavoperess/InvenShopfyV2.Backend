using InvenShopfy.Core.Requests.Products.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface IUnitHandler
{
    Task<Response<Models.Product.Unit?>> CreateProductUnitAsync(CreateUnitRequest request);
    Task<Response<Models.Product.Unit?>> UpdateProductUnitAsync(UpdateUnitRequest request);
    Task<Response<Models.Product.Unit?>> DeleteProductUnitAsync(DeleteUnitRequest request);
    Task<Response<Models.Product.Unit?>> GetProductUnitByIdAsync(GetUnitByIdRequest request);
    Task<PagedResponse<List<Models.Product.Unit>?>> GetProductUnitByPeriodAsync(GetAllUnitRequest request);
}