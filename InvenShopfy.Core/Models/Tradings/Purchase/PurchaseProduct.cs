using System.Text.Json.Serialization;

namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class PurchaseProduct
{
    public long AddPurchaseId { get; init; }

    [JsonIgnore]
    public AddPurchase AddPurchase { get; init; } = null!;
    
    public long ProductId { get; init; }
    public Product.Product Product { get; init; } = null!;
    public decimal TotalInTaxPaidPerProduct { get; init; }
    public int TotalQuantityBoughtPerProduct { get; init; }
    public decimal TotalPricePaidPerProduct { get; init; }
    public string PurchaseReferenceNumber { get; init; } = string.Empty;
}