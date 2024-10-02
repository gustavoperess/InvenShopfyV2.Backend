namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class PurchaseProduct
{
    public long PurchaseId { get; set; }
    public long ProductId { get; set; }
    public Product.Product Product { get; set; } = null!;
    public int TotalQuantityBoughtPerProduct { get; set; }
    public double TotalPricePaidPerProduct { get; set; }
    public string PurchaseReferenceNumber { get; set; } = null!;
}