using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class Add
{
    public long Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    public long Warehouse { get; set; } 
    public Warehouse.Warehouse WarehouseId { get; set; } = null!;
    
    public long Supplier { get; set; }
    public Supplier SupplierId { get; set; } = null!;
    
    public long Product { get; set; } 
    public Product.Product ProductId { get; set; } = null!;
    
    public EPurchaseStatus PurchaseStatus { get; set; }
    
    public int ShippingCost { get; set; }
    
    public string PurchaseNote { get; set; } = String.Empty;
    
    public string UserId { get; set; } = string.Empty;
}