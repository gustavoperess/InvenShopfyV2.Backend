using InvenShopfy.Core.Requests.Tradings.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.SalesReturn;

public interface ISalesReturnHandler
{
    Task<Response<Models.Tradings.SalesReturn.SaleReturn?>> CreateSalesReturnAsync(CreateSalesReturnRequest request);
    Task<Response<List<Models.Tradings.SalesReturn.SalesReturnByName>?>> GetSalesPartialByCustomerNameAsync(GetSalesReturnByCustomerName request);
    
    
    
}