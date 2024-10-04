using InvenShopfy.Core.Requests.Reports.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Reports.Sales;

public interface ISalesReportHandler
{
    Task<PagedResponse<List<Models.Reports.SaleReport>?>> GetByPeriodAsync(GetSalesReportByDateRequest request);
    Task<PagedResponse<List<Models.Reports.SaleReport>?>> GetByWarehouseNameAsync(GetSalesReportByWarehouseRequest request);
}