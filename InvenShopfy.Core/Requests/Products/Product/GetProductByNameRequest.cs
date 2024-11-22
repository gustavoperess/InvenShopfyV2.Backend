namespace InvenShopfy.Core.Requests.Products.Product;

public class GetProductByNameRequest : PagedRequest
{
    public string ProductName { get; set; } = string.Empty;
}