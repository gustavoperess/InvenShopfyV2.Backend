using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class Add
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    public string Warehouse { get; set; } = String.Empty;
    public Warehouse.Warehouse WarehouseId { get; set; } = null!;
    
    public string Supplier { get; set; } = String.Empty;
    public Supplier SupplierId { get; set; } = null!;
    
    public string Product { get; set; } = String.Empty;
    public Product.Product ProductId { get; set; } = null!;
    
    public EPurchaseStatus PurchaseStatus { get; set; }
    
    public int ShippingCost { get; set; }
    
    public string PurchaseNote { get; set; } = String.Empty;
}