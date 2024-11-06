namespace InvenShopfy.Core.Models.Expenses.Dto;

public sealed class ExpenseDto
{
    public long Id { get; init; }
    public string ExpenseDescription { get; set; } = string.Empty;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string WarehouseName { get; set; } = string.Empty;
    public string ExpenseType { get; set; } = string.Empty;
    public string ExpenseCategory { get; set; } = string.Empty;
    public long VoucherNumber { get; set; }
    public decimal ExpenseCost { get; set; }
    public string ExpenseNote { get; set; } = String.Empty;
    public decimal ShippingCost { get; set; } 
  
}