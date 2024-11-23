namespace InvenShopfy.Core.Models.Tradings.Sales.Dto;

public sealed class SalePopUp
{
    public long ProductId { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalPricePerProduct { get; set; }
    public string ProductName { get; set; } = null!;
    public string UnitShortName { get; set; } = null!;
    public int TotalQuantitySoldPerProduct { get; set; }
    public string ReferenceNumber { get; set; } = null!;
    public decimal ShippingCost { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerPhoneNumber { get; set; }
    public string? CustomerAddress { get; set; }
    public string? SaleStatus { get; set; }
    public string? SaleNote { get; set; }
    public DateOnly? SaleDate { get; set; }
    public string? WarehouseName { get; set; }
    public string? BillerPhoneNumber { get; set; }
    public string? StaffNote { get; set; }
    public string? CustomerName { get; set; }
    public string? BillerName { get; set; }
    public string? BillerEmail { get; set; }

}