namespace InvenShopfy.Core.Models.Reports;

public class CustomerReport
{
    public long Id { get; set; }
    public string? CustomerName { get; set; }
    public long? RewardPoints { get; set; }
    public int NumberOfPurchases { get; set; }
    public decimal TotalPaidInShipping { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal TotalPaidInTaxes { get; set; }
    public int NumberOfProductsBought { get; set; }
    public DateOnly? LastPurchase { get; set; }
}
