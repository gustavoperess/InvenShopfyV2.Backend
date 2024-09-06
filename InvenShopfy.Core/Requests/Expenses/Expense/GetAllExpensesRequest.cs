namespace InvenShopfy.Core.Requests.Expenses.Expense;

public class GetAllExpensesRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}