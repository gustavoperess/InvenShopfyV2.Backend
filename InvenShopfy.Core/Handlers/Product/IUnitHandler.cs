using InvenShopfy.Core.Requests.Products.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface IUnitHandler
{
    Task<Response<Models.Product.Unit?>> CreateAsync(CreateUnitRequest request);
    Task<Response<Models.Product.Unit?>> UpdateAsync(UpdateUnitRequest request);
    Task<Response<Models.Product.Unit?>> DeleteAsync(DeleteUnitRequest request);
    Task<Response<Models.Product.Unit?>> GetByIdAsync(GetUnitByIdRequest request);
    Task<PagedResponse<List<Models.Product.Unit>?>> GetByPeriodAsync(GetAllUnitRequest request);
}