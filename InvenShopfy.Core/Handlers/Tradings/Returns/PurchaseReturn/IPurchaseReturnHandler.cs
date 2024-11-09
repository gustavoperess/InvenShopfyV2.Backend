using InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn.Dto;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;

public interface IPurchaseReturnHandler
{
    Task<Response<Models.Tradings.Returns.PurchaseReturn.PurchaseReturn?>> CreatePurchaseReturnAsync(CreatePurchaseReturnRequest request);
    Task<PagedResponse<List<Models.Tradings.Returns.PurchaseReturn.PurchaseReturn>?>> GetAllPurchaseReturnAsync(GetAllPurchaseReturnsRequests request);
    Task<Response<Models.Tradings.Returns.PurchaseReturn.PurchaseReturn?>> DeletePurchaseReturnAsync(DeletePurchaseReturnRequest request);
    Task<Response<List<PurchaseReturnByReturnNumber>?>> GetPurchasePartialByReferenceNumberAsync(GetPurchaseReturnByNumberRequest request);
    Task <Response<decimal?>> GetTotalPurchaseReturnAsync(GetAllPurchaseReturnsRequests request);

}