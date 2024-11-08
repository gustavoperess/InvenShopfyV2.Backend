using InvenShopfy.Core.Models.Tradings.Purchase.Dto;
using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Purchase;

public interface IPurchaseHandler
{
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> CreatePurchaseAsync(CreatePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> UpdatePurchaseAsync(UpdatePurchaseRequest request);
    Task<Response<Models.Tradings.Purchase.AddPurchase?>> DeletePurchaseAsync(DeletePurchaseRequest request);
    Task<Response<List<PurchasePerProduct>?>> GetPurchaseByIdAsync(GetPurchaseByIdRequest request);
    Task<PagedResponse<List<PurchaseList>?>> GetPurchaseByPeriodAsync(GetAllPurchasesRequest request);
    Task<Response<List<PurchaseDashboard>?>> GetPurchaseStatusDashboardAsync(GetAllPurchasesRequest request);
}