namespace InvenShopfy.Core.Models.Expenses;
using System.ComponentModel.DataAnnotations;
public enum CustomerGroup
{
    [Display(Name = "Direct Expense")]
    DirectExpense,
        
    [Display(Name = "Draft Expense")]
    DraftExpense
}

public class Expense
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    public CustomerGroup ExpenseType { get; set; }
    
    public long ExpenseCategoryId { get; set; }
    public ExpenseCategory ExpenseCategory { get; set; } = null!;
    public long VoucherNumber { get; set; }
    public double Amount { get; set; }
    public string PurchaseNote { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
}