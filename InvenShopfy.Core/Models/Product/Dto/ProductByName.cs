namespace InvenShopfy.Core.Models.Product.Dto;

public sealed class ProductByName
{
    public long Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public int ProductCode { get; set; }
    public string ProductImage { get; set; } = null!;
    public int StockQuantity { get; init; }
    public int TaxPercentage { get; set; }
    
    public string MarginRange { get; set; } = string.Empty;
    public string Category { get; set; } = null!;
    public string Subcategory { get; set; } = string.Empty;
    public bool Expired { get; set; }
    public string UserId { get; set; } = string.Empty;
}