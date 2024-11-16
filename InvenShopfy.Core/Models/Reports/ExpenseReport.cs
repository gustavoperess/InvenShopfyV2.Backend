namespace InvenShopfy.Core.Models.Reports;

public class ExpenseReport
{
    public string? Name { get; set; }
    public long Id { get; set; }
    public decimal TotalCost { get; set; }
    public int NumberOfTimesUsed { get; set; }
    public decimal ShippingCost  { get; set; }
    
}
