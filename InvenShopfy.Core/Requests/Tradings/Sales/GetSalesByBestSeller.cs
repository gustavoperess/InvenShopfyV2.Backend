namespace InvenShopfy.Core.Requests.Tradings.Sales;

public class GetSalesByBestSeller : PagedRequest
{
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}