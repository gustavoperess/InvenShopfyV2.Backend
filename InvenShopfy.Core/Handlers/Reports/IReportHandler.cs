using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Reports;

public interface IReportHandler
{
    Task<PagedResponse<List<Models.Reports.SaleReport>?>> GetSalesReportAsync(GetReportRequest request);
    
    Task<PagedResponse<List<Models.Reports.PurchaseReport>?>> GetPurchaseReportAsync(GetReportRequest request);
    
    Task<PagedResponse<List<Models.Reports.ProductReport>?>> GetProductReportAsync(GetReportRequest request);
    
    Task<PagedResponse<List<Models.Reports.CustomerReport>?>> GetCustomerReportAsync(GetReportRequest request);


}