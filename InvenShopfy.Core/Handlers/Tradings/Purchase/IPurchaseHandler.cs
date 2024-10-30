using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Purchase;

public interface IPurchaseHandler
{
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> CreatePurchaseAsync(CreatePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> UpdatePurchaseAsync(UpdatePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> DeletePurchaseAsync(DeletePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> GetPurchaseByIdAsync(GetPurchaseByIdRequest request);
    Task<PagedResponse<List<Models.Tradings.Purchase.PurchaseList>?>> GetPurchaseByPeriodAsync(GetAllPurchasesRequest request);
}