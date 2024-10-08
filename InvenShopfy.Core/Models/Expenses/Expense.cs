using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.Expenses;



public class Expense
{
    public Expense(long id, DateOnly date, Warehouse.Warehouse warehouse, string expenseType, ExpenseCategory 
        expenseCategory, long voucherNumber, decimal amount, string purchaseNote, string userId)
    {
        Id = id;
        Date = date;
        Warehouse = warehouse;
        ExpenseType = expenseType;
        ExpenseCategory = expenseCategory;
        VoucherNumber = voucherNumber;
        Amount = amount;
        PurchaseNote = purchaseNote;
        UserId = userId;
    }
    public Expense()
    {
    }
    
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