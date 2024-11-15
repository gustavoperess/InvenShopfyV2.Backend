using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Reports;

public interface IReportHandler
{
    Task<PagedResponse<List<Models.Reports.SaleReport>?>> GetSalesReportAsync(GetSalesReportRequest request);
}