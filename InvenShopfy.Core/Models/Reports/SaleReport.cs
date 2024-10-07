namespace InvenShopfy.Core.Models.Reports;

public class SaleReport
{
    public long Id { get; set; }
    public DateOnly SalesDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string Warehouse { get; set; } = null!;
    
    public int NumberOfProductsSold { get; set; }

    public double TotalAmountSold { get; set; }

    public string SaleStatus { get; set; } = String.Empty;
}