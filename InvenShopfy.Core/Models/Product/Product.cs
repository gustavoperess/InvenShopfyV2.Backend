namespace InvenShopfy.Core.Models.Product;

public class Product
{

    public long Id { get; init; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public int ProductCode { get; set; }
    public int StockQuantity { get; set; }
    public DateOnly CreateAt { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    public long UnitId  { get; set; }
    public Unit Unit { get; init; } = null!;
    public long BrandId { get; set; }
    public Brand Brand { get; init; } = null!;
    public long CategoryId { get; set; }
    public Category Category { get; init; } = null!;
    public string ProductImage { get; set; } = null!;
    // public string UserId { get; init; } = string.Empty;
    public int TaxPercentage { get; set; }
    public string MarginRange { get; set; } = null!;
    public string SubCategory { get; set; } = string.Empty;
    public bool Featured { get; init; }
    public bool Expired { get; init; }
    public bool Sale { get; init; }
}