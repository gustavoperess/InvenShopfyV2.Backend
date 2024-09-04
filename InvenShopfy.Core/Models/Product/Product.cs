namespace InvenShopfy.Core.Models.Product;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    
    public int ProductCode { get; set; }
    
    public long ProductTypeId { get; set; }
    // public  ProductType { get; set; }
    
    public DateTime CreateAt { get; set; } = DateTime.Now;

    public long ProductUnitId  { get; set; }
    // public ProductUnit ProductUnit { get; set; }

    public long BrandId { get; set; }
    // public Brand Brand{ get; set; }
    
    public long CategoryId { get; set; }
    // public Category Category { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;
    
}