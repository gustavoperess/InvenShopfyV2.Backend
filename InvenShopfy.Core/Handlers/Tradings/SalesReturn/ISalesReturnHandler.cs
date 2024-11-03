using InvenShopfy.Core.Requests.Tradings.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.SalesReturn;

public interface ISalesReturnHandler
{
    Task<Response<Models.Tradings.SalesReturn.SaleReturn>> CreateReturn(CreateSalesReturnRequest request);
    
    
}