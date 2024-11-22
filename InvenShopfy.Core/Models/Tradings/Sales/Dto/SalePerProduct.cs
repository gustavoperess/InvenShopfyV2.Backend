namespace InvenShopfy.Core.Models.Tradings.Sales.Dto;

public sealed class SalePerProduct
{
    public long ProductId { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalPricePerProduct { get; set; }
    public string ProductName { get; set; } = null!;
    public string UnitShortName { get; set; } = null!;
    public int TotalQuantitySoldPerProduct { get; set; }
    public string ReferenceNumber { get; set; } = null!;
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }
    public string? BrandName { get; set; }
    public string? ProductImage { get; set; }
    public bool Featured { get; set; }
    public decimal ProfitAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal Discount { get; set; }
    public string? StaffNote { get; set; }
    public string? SaleNote { get; set; }
    public string? BillerName { get; set; }
    public string? BillerEmail { get; set; }

}