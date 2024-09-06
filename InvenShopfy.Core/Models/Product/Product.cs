namespace InvenShopfy.Core.Models.Product;

public class Product
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Price { get; set; }
    
    public int ProductCode { get; set; }
    
    public double Quantity { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;

    public long UnitId  { get; set; }
    public Unit Unit { get; set; } = null!;

    public long BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public string ProductImage { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;
    
}