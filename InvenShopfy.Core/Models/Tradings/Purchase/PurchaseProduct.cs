using System.Text.Json.Serialization;

namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class PurchaseProduct
{
    public long AddPurchaseId { get; set; }

    [JsonIgnore]
    public AddPurchase AddPurchase { get; set; } = null!;
    
    public long ProductId { get; set; }
    public Product.Product Product { get; set; } = null!;
    
    public int TotalQuantityBoughtPerProduct { get; set; }
    public decimal TotalPricePaidPerProduct { get; set; }
    public string PurchaseReferenceNumber { get; set; } = string.Empty;
}