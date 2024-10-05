using InvenShopfy.Core.Common.RandomNumber;
using InvenShopfy.Core.Models.People;


namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class AddPurchase
{
    public long Id { get; set; }
    
    public DateTime PurchaseDate { get; set; }
    
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    
    public long SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    
    public string PurchaseStatus { get; set; } = string.Empty;
    
    public decimal ShippingCost { get; set; }
    
    public string PurchaseNote { get; set; } = string.Empty;
    
    public string ReferenceNumber { get; private set; } = GenerateRandomNumber.RandomNumberGenerator();
    public string UserId { get; set; } = string.Empty;
    
    public List<PurchaseProduct> PurchaseProducts { get; set; } = new List<PurchaseProduct>();
    public decimal TotalAmountBought { get; set; }
    
    public int TotalNumberOfProductsBought { get; set; }
    
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