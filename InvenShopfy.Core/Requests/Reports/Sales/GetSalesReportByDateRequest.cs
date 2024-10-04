namespace InvenShopfy.Core.Requests.Reports.Sales;

public class GetSalesReportByDateRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}