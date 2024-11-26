namespace InvenShopfy.Core.Requests.Expenses.Expense;

public class GetExpenseByNumberRequest : PagedRequest
{
    public string ExpenseNumber { get; set; } = string.Empty;
}