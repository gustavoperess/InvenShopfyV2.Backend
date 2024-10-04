namespace InvenShopfy.Core.Requests.Tradings.Sales;

public class GetSalesByBestSeller : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}