namespace InvenShopfy.Core.Models.Expenses.Dto;

public class ExpenseDashboard
{
    public long Id { get; set; }
    public DateOnly Date { get; set; } 
    public long VoucherNumber { get; set; }
    public decimal ExpenseCost { get; set; }
    public string ExpenseType { get; set; } = null!;
    public string ExpenseStatus { get; set; } = null!;
    public string ExpenseDescription { get; set; } = null!;
    
    public string ExpenseCategory { get; set; } = null!;

}