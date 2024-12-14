namespace InvenShopfy.Core.Models.Reports;

public class SupplierReport
{
    public long SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    
    public string CompanyName { get; set; } = string.Empty;
    public int NumberOfPurchases { get; set; }
    public int NumberOfProductsBought { get; set; }
    public decimal TotalPaidInTaxes { get; set; }
    public decimal TotalPaidInShipping { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public decimal TotalAmount { get; set; }
}