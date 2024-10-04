namespace InvenShopfy.Core.Requests.Reports;

public class GetSalesReportRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}