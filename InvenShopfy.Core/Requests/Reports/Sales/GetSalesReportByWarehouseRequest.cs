namespace InvenShopfy.Core.Requests.Reports.Sales;

public class GetSalesReportByWarehouseRequest : PagedRequest
{
    public string WarehouseName = string.Empty;
}