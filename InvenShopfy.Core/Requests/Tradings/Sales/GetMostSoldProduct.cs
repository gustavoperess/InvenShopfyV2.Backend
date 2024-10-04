namespace InvenShopfy.Core.Requests.Tradings.Sales;

public class GetMostSoldProduct : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}