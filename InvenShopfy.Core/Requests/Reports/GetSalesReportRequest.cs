namespace InvenShopfy.Core.Requests.Reports;

public class GetSalesReportRequest : PagedRequest
{
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}