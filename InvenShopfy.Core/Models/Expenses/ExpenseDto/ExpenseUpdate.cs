namespace InvenShopfy.Core.Models.Expenses.ExpenseDto;

public class ExpenseUpdate
{
    public long Id { get; init; }
    public string ExpenseCategory { get; set; }= null!;
    public string VoucherNumber { get; set; } = null!;
    public decimal ExpenseCost { get; set; }
    public string PaymentStatus { get; set; } = null!;
}