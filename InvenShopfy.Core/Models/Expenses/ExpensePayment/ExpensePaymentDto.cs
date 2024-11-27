namespace InvenShopfy.Core.Models.Expenses.ExpensePayment;

public class ExpensePaymentDto
{
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string VoucherNumber { get; init; } = null!;
    public decimal ExpenseCost { get; set; }
    public string ExpenseCategory  { get; set; } = null!;
    public string PaymentType { get; set; } =  null!;
    public string CardNumber { get; set; } = null!;

}