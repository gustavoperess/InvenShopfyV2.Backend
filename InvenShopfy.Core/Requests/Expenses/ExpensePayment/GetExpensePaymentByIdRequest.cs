namespace InvenShopfy.Core.Requests.Expenses.ExpensePayment;

public class GetExpensePaymentByIdRequest : Request
{
    public long ExpenseId { get; set; }
}