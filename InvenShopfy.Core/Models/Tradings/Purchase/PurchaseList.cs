namespace InvenShopfy.Core.Models.Tradings.Purchase;

public class PurchaseList
{
    public long Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string SupplierName { get; set; }  = string.Empty;
    public string WarehouseName { get; set; }  = string.Empty;
    public string PurchaseStatus { get; set; } = string.Empty;
    public decimal ShippingCost { get; set; }
    public decimal TotalAmountBought { get; set; }
    public string ReferenceNumber { get;  set; }  = string.Empty;

    public int TotalNumberOfProductsBought { get; set; }
}