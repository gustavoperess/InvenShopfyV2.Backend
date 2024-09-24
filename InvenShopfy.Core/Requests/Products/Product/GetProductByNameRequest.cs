namespace InvenShopfy.Core.Requests.Products.Product;

public class GetProductByNameRequest : Request
{
    public string Title { get; set; } = string.Empty;
}