using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class AddPurchase
{
    public long Id { get; set; }
    
    private static readonly Random RandomNumber = new Random();
    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    
    public long WarehouseId { get; set; } 
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    
    public long SupplierId { get; set; }
    public Supplier Supplier{ get; set; } = null!;
    
    public long ProductId { get; set; } 
    public Product.Product Product { get; set; } = null!;
    
    public string PurchaseStatus { get; set; } = string.Empty;
    
    public int ShippingCost { get; set; }
    
    public string PurchaseNote { get; set; } = String.Empty;
    
    public string ReferenceNumber { get; private set; }
    public string UserId { get; set; } = string.Empty;
    
    public List<PurchaseProduct> PurchaseProduct { get; set; } = new List<PurchaseProduct>();
    public int TotalQuantityBought { get; set; }
    
    public AddPurchase()
    {
        ReferenceNumber = GenerateRandomNumber(); 
    }
        
    private static string GenerateRandomNumber()
    {
        char letter = (char)RandomNumber.Next('A', 'Z' + 1); 
        int randNum = RandomNumber.Next(1000000); 
        return letter + "-" + randNum.ToString("D6"); 
    }
    
    
    public PurchaseProduct CreatePurchaseProduct(long productId, double totalPricePerProduct, int totalQuantitySoldPerProduct)
    {
        var purchaseProduct = new PurchaseProduct
        {
            ProductId = productId,
            AddPurchase = this,
            TotalPricePaidPerProduct = totalPricePerProduct,
            TotalQuantityBoughtPerProduct = totalQuantitySoldPerProduct,
            PurchaseReferenceNumber = ReferenceNumber 
        };
        return purchaseProduct;
    }
}