namespace InvenShopfy.Core.Requests.Reports.Sales;

public class GetSalesReportByDateRequest : PagedRequest
{
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}