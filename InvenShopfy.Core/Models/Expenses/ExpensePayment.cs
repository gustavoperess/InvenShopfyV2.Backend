using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.Expenses;

public class ExpensePayment
{
    public long Id { get; set; }

    public long ExpenseId { get; set; }
    public Expense Expense { get; init; } = null!;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string ExpensePaymentDescription { get; set; } = null!;
    public string PaymentType { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public long ExpenseCategoryId { get; set; }
    public string ExpenseNote { get; set; } = String.Empty;
    public string ExpenseStatus { get; set; } = EPaymentStatus.Completed.ToString();
    public string UserId { get; set; } = string.Empty;
}