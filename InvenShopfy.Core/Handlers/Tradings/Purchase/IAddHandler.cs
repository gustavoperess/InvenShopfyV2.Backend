using InvenShopfy.Core.Requests.Tradings.Purchase.Add;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Purchase;

public interface IAddHandler
{
    Task<Response<Models.Tradings.Purchase.Add?>> CreateAsync(CreatePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.Add?>> UpdateAsync(UpdatePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.Add?>> DeleteAsync(DeletePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.Add?>> GetByIdAsync(GetPurchaseByIdRequest request);
    Task<PagedResponse<List<Models.Tradings.Purchase.Add?>?>> GetByPeriodAsync(GetAllPurchasesRequest request);
}