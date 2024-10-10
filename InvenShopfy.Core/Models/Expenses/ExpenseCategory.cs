namespace InvenShopfy.Core.Models.Expenses;

public class ExpenseCategory
{
    public long Id { get; init; }
    public string Category { get; set; } = String.Empty;
    public string SubCategory { get; set; } = String.Empty;
    public string UserId { get; init; } = string.Empty;
}