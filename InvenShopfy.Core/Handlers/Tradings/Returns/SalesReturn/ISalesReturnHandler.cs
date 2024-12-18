using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn.Dto;
using InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;

public interface ISalesReturnHandler
{
    Task<Response<SaleReturn?>> CreateSalesReturnAsync(CreateSalesReturnRequest request);
    Task<PagedResponse<List<SaleReturn>?>> GetAllSalesReturnAsync(GetAllSalesReturnsRequest request);
    Task<Response<SaleReturn?>> DeleteSalesReturnAsync(DeleteSalesReturnRequest request);
    Task<Response<List<SalesReturnByReturnNumber>?>> GetSalesPartialByReferenceNumberAsync(GetSalesReturnByNumberRequest request);
    Task<Response<List<SalesReturnDashboard>?>> GetSaleReturnDashboardAsync(GetAllSalesReturnsRequest request);
    Task<Response<SaleReturnById>> GetSalesReturnByIdAsync(GetSalesReturnByIdRequest request);
    Task <Response<decimal?>> GetTotalSalesReturnAsync(GetAllSalesReturnsRequest request);

}