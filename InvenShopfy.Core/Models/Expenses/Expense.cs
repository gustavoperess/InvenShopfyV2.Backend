using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.Expenses;



public class Expense
{
    public long Id { get; set; }
    public string ExpenseDescription { get; set; } = null!;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    public string ExpenseType { get; set; } = string.Empty;
    public long ExpenseCategoryId { get; set; }
    public ExpenseCategory ExpenseCategory { get; set; } = null!;
    public long VoucherNumber { get; set; }
    public decimal ExpenseCost { get; set; }
    public string ExpenseNote { get; set; } = String.Empty;
    
    public decimal ShippingCost { get; set; } 
    public string UserId { get; set; } = string.Empty;
}