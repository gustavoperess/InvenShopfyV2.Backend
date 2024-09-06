namespace InvenShopfy.Core.Models.Expenses;

public class Expense
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    public List<string> ExpenseType { get; set; } = new List<string> { "Direct Expense", "Draft Expense" };
    public long ExpenseCategoryId { get; set; }
    public ExpenseCategory ExpenseCategory { get; set; } = null!;

    public double VoucherNumber { get; set; }
    public double Amount { get; set; }
    public string PurchaceNote { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
}