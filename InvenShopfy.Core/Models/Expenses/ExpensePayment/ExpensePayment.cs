using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.Expenses.ExpensePayment;

public class ExpensePayment
{
    public long Id { get; set; }
    public long ExpenseId { get; set; }
    public Expense.Expense Expense { get; init; } = null!;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string PaymentType { get; set; }  = EPaymentType.Cash.ToString();
    public string CardNumber { get; set; } = string.Empty;
    public string ExpenseNote { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
}