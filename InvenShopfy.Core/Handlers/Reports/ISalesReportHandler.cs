using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Reports;

public interface ISalesReportHandler
{
    Task<PagedResponse<List<Models.Reports.SaleReport>?>> GetByPeriodAsync(GetSalesReportRequest request);
}