namespace InvenShopfy.Core.Models.Reports;

public class SaleReport
{
    public long Id { get; set; }
    public DateTime SalesDate { get; set; }

    public long WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    
    public int NumberOfProductsSold { get; set; }

    public decimal TotalAmountSold { get; set; }

    public string SaleStatus { get; set; } = String.Empty;
}