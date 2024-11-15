namespace InvenShopfy.Core.Models.Reports;

public class ProductReport
{
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
    public int TotalQuantityBought { get; set; }
    public decimal TotalAmountPaid { get; set; }
    public decimal TotalPaidInTaxes { get; set; }
    public int TotalQuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
    public int StockQuantity { get; set; }
}