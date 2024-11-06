namespace InvenShopfy.Core.Models.Product.Dto;

public sealed class ProductList
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ProductCode { get; set; }
    public string ProductImage { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string SubCategories { get; set; } = string.Empty;

}