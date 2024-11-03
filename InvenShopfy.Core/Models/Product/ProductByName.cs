namespace InvenShopfy.Core.Models.Product;

public class ProductByName
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ProductCode { get; set; }
    public int StockQuantity { get; init; }
    public string Category { get; set; } = null!;
    public string ProductImage { get; set; } = null!;
    public string Subcategory { get; set; } = string.Empty;
    public bool Expired { get; set; }
    public string UserId { get; set; } = string.Empty;
}