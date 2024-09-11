using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class AddPurchase
{
    public long Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    public long WarehouseId { get; set; } 
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    
    public long SupplierId { get; set; }
    public Supplier Supplier{ get; set; } = null!;

    
    public long ProductId { get; set; } 
    public Product.Product Product { get; set; } = null!;
    
    public EPurchaseStatus PurchaseStatus { get; set; }
    
    public int ShippingCost { get; set; }
    
    public string PurchaseNote { get; set; } = String.Empty;
    
    public string UserId { get; set; } = string.Empty;
}