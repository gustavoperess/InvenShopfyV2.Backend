namespace InvenShopfy.Core.Requests.Reports;

public class GetSalesReportRequest : PagedRequest
{
    public string? DateRange { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}