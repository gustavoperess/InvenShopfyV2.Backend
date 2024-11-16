namespace InvenShopfy.Core.Models.Reports;

public class ProductReport
{
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }
    public int TotalQuantityBought { get; set; }
    public decimal TotalAmountPaid { get; set; }
    public decimal TotalPaidInTaxes { get; set; }
    public int TotalQuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
    public int StockQuantity { get; set; }
    
}


// public long Id { get; set; }
// public string? ProductName { get; set; }
// public string? SupplierName { get; set; }
//     
// public string? WarehouseName { get; set; }
// public DateOnly? PurchaseDate { get; set; }
// public int TotalQuantityBoughtPerProduct { get; set; }
// public decimal TotalPricePaidPerProduct { get; set; }
// public decimal TotalInTaxPaidPerProduct { get; set; }
// public string? PurchaseReferenceNumber { get; set; }
