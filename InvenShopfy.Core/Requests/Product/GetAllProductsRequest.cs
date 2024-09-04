namespace InvenShopfy.Core.Requests.Product;

public class GetAllProductsRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}