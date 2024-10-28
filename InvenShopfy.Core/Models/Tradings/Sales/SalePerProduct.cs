namespace InvenShopfy.Core.Models.Tradings.Sales;

public class SalePerProduct
{
    public long ProductId { get; set; }

    public decimal ProductPrice { get; set; }
    public decimal TotalPricePerProduct { get; set; }
    public string ProductName { get; set; } = null!;
    public string UnitShortName { get; set; } = null!;
    public int TotalQuantitySoldPerProduct { get; set; }
    public string ReferenceNumber { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public decimal Discount { get; set; }
}