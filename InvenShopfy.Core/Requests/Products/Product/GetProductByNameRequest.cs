namespace InvenShopfy.Core.Requests.Products.Product;

public class GetProductByNameRequest : PagedRequest
{
    public string Title { get; set; } = string.Empty;
}