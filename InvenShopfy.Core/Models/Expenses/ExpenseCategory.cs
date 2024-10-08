namespace InvenShopfy.Core.Models.Expenses;

public class ExpenseCategory
{
    public ExpenseCategory(long id, string category, string subCategory, string userId)
    {
        Id = id;
        Category = category;
        SubCategory = subCategory;
        UserId = userId;
    }
    public ExpenseCategory()
    {
    }
    public long Id { get; set; }
    public string Category { get; set; } = String.Empty;
    public string SubCategory { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
}