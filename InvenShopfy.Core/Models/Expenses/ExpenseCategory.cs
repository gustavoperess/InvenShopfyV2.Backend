namespace InvenShopfy.Core.Models.Expenses;

public class ExpenseCategory
{
    public long Id { get; set; }
    public string Category { get; set; } = String.Empty;
    public string SubCategory { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
}