namespace InvenShopfy.Core.Models.Expenses.ExpenseDto;

public class ExpenseDashboard
{
    public long Id { get; set; }
    public DateOnly Date { get; set; }
    public string VoucherNumber { get; set; } = null!;
    public decimal ExpenseCost { get; set; }
    public string ExpenseType { get; set; } = null!;
    public string ExpenseStatus { get; set; } = null!;
    public string ExpenseDescription { get; set; } = null!;
    public string ExpenseCategory { get; set; } = null!;

}