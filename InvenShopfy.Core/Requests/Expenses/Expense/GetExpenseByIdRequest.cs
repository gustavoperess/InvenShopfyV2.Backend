namespace InvenShopfy.Core.Requests.Expenses.Expense;

public class GetExpenseByIdRequest : Request
{
    public long Id { get; set; }
}