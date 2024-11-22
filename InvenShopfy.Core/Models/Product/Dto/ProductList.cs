namespace InvenShopfy.Core.Models.Product.Dto;

public sealed class ProductList
{
    public long Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public int ProductCode { get; set; }
    public string ProductImage { get; set; } = string.Empty;
    public string MarginRange { get; set; } = string.Empty;
    public int TaxPercentage { get; set; }
    public int StockQuantity { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string SubCategories { get; set; } = string.Empty;

}