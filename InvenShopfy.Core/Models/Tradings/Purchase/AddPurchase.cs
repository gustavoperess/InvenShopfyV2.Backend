using InvenShopfy.Core.Common.RandomNumber;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Responses;


namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class AddPurchase
{
    public long Id { get; init; }
    
    public DateOnly PurchaseDate { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; init; } = null!;
    
    public long SupplierId { get; set; }
    public Supplier Supplier { get; init; } = null!;
    
    public string PurchaseStatus { get; set; } = string.Empty;
    
    public decimal ShippingCost { get; set; }
    
    public string PurchaseNote { get; set; } = string.Empty;
    
    public string ReferenceNumber { get; private set; } = GenerateRandomNumber.RandomNumberGenerator();
    public string UserId { get; init; } = string.Empty;
    
    public decimal TotalAmountBought { get; set; }
    
    public int TotalNumberOfProductsBought { get; set; }
    public List<PurchaseProduct> PurchaseProducts { get; init; } = new List<PurchaseProduct>();
    
    public Response<AddPurchase?> AddPurchaseToPurchase(Dictionary<long, int> productIdPlusQuantity, List<Product.Product> purchase)
    {
 
        int sumOfItems = 0;
        foreach (var item in productIdPlusQuantity)
        {
            var product = purchase.FirstOrDefault(p => p.Id == item.Key);
            if (product == null)
            {
                return new Response<AddPurchase?>(null, 400, $"Product with Id {item.Key} not found");
            }
            if (item.Value <= 0)
            {
                continue; 
            }
          
            var pricePerProduct = product.Price * item.Value;
            var purchaseProduct = CreatePurchaseProduct(product.Id, pricePerProduct, item.Value);
            product.StockQuantity += item.Value;
            sumOfItems += item.Value;
            PurchaseProducts.Add(purchaseProduct);
     
        }

        TotalNumberOfProductsBought = sumOfItems;
        return new Response<AddPurchase?>(this, 200, "Products added to sale successfully");
    }
    
    private PurchaseProduct CreatePurchaseProduct(long productId, decimal totalPricePerProduct, int totalQuantitySoldPerProduct)
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