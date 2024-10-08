namespace InvenShopfy.Core.Models.Product;

public class Product
{
    public Product()
    {
    }
    public Product(long id, decimal price, int productCode, int stockQuantity, long unitId, long brandId, long categoryId, string productImage, bool featured, bool differPriceWarehouse, bool expired, bool sale, string  userId)
    {
        Id = id;
        Price = price;
        ProductCode = productCode;
        StockQuantity = stockQuantity;
        UnitId = unitId;
        BrandId = brandId;
        CategoryId = categoryId;
        ProductImage = productImage;
        Featured = featured;
        DifferPriceWarehouse = differPriceWarehouse;
        Expired = expired;
        Sale = sale;
        UserId = userId;
    }

    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    public int ProductCode { get; set; }
    
    public int StockQuantity { get; set; }
    public DateOnly CreateAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public long UnitId  { get; set; }
    public Unit Unit { get; set; } = null!;

    public long BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public string ProductImage { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;

    public string Subcategory { get; set; } = string.Empty;
    public bool Featured { get; set; }
    public bool DifferPriceWarehouse { get; set; }
    public bool Expired { get; set; }
    public bool Sale { get; set; }
}