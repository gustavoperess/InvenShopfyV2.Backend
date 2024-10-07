namespace InvenShopfy.Core.Requests.Tradings.Sales;

public class GetMostSoldProduct : PagedRequest
{
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}