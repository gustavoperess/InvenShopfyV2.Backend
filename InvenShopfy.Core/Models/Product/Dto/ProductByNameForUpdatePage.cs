namespace InvenShopfy.Core.Models.Product.Dto;

public class ProductByNameForUpdatePage
{
    public long Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public int ProductCode { get; set; }
    public string ProductImage { get; set; } = null!;
    public int TaxPercentage { get; set; }
    public string MarginRange { get; set; } = string.Empty;
    public long MainCategoryId { get; set; } 
    public string BrandName { get; set; } = null!;
    public string UnitName { get; set; } = null!;
    public bool Expired { get; set; }
    public string UserId { get; set; } = string.Empty;
}