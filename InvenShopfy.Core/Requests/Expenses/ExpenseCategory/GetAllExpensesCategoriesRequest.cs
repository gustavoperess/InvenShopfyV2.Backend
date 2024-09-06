namespace InvenShopfy.Core.Requests.Expenses.ExpenseCategory;

public class GetAllExpensesCategoriesRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}