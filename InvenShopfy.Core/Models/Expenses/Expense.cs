using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.Expenses;



public class Expense
{
    
    public long Id { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    public string ExpenseType { get; set; } = string.Empty;
    public long ExpenseCategoryId { get; set; }
    public ExpenseCategory ExpenseCategory { get; set; } = null!;
    public long VoucherNumber { get; set; }
    public decimal Amount { get; set; }
    public string PurchaseNote { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
}