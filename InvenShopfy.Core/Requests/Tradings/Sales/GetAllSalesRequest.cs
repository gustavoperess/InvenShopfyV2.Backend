namespace InvenShopfy.Core.Requests.Tradings.Sales;

public class GetAllSalesRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}