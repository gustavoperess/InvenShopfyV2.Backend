namespace InvenShopfy.Core.Requests.Products.Product;

public class GetAllProductsRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}