namespace InvenShopfy.Core.Models.Reports;

public class WarehouseReport
{
    public long Id { get; set; }
    public string? WarehouseName { get; set; }
    public decimal TotalAmountBought { get; set; }
    public decimal TotalNumbersOfProductsBought { get; set; }
    public decimal TotalPaidInShipping { get; set; }
    public decimal TotalAmountSold { get; set; }
    public decimal TotalQtyOfProductsSold { get; set; }
    public int TotalNumberOfSales { get; set; }
    public decimal TotalProfit { get; set; }
    public int StockQuantity { get; set; }
    
}

