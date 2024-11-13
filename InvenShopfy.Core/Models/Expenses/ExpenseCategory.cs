namespace InvenShopfy.Core.Models.Expenses;

public class ExpenseCategory
{
    public long Id { get; init; }
    public string MainCategory { get; set; } = String.Empty;
    public List<string> SubCategory { get; set; } = new List<string>();
    public string UserId { get; init; } = string.Empty;
}