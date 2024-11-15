namespace InvenShopfy.Core.Models.Reports;

public class SaleReport
{
    public long BillerId { get; set; }
    public int TotalQuantitySold { get; set; }
    public decimal TotalTaxPaid { get; set; }
    public decimal TotalProfit { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal TotalAmount { get; set; }
    
    
    
}