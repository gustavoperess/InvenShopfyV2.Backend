using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Purchase;

public interface IPurchaseHandler
{
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> CreateAsync(CreatePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> UpdateAsync(UpdatePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> DeleteAsync(DeletePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> GetByIdAsync(GetPurchaseByIdRequest request);
    Task<PagedResponse<List<Models.Tradings.Purchase.AddPurchase>?>> GetByPeriodAsync(GetAllPurchasesRequest request);
}