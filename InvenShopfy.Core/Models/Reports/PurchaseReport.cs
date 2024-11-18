namespace InvenShopfy.Core.Models.Reports;

public class PurchaseReport
{
    public string? TempKey { get; set; } 
    public long Id { get; set; }
    public string? ProductName { get; set; }
    public string? SupplierName { get; set; }

    public string? WarehouseName { get; set; }
    public DateOnly? PurchaseDate { get; set; }
    public int TotalQuantityBoughtPerProduct { get; set; }
    public decimal TotalPricePaidPerProduct { get; set; }
    public decimal TotalInTaxPaidPerProduct { get; set; }
    public string? PurchaseReferenceNumber { get; set; }
}