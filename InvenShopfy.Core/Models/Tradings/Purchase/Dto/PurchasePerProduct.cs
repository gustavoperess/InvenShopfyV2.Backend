namespace InvenShopfy.Core.Models.Tradings.Purchase.Dto;

public sealed class PurchasePerProduct
{
    public long ProductId { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalPricePaidPerProduct { get; set; }
    public string ProductName { get; set; } = null!;
    public string UnitShortName { get; set; } = null!;
    public int TotalQuantityBoughtPerProduct { get; set; }
    public string ReferenceNumber { get; set; } = null!;
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }
    public string? PurchaseNote { get; set; }
    public string SupplierName { get; set; } = null!;
    public string SupplierEmail { get; set; } = null!;
}