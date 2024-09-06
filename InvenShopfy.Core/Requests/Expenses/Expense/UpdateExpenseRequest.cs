using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Models.Expenses;

namespace InvenShopfy.Core.Requests.Expenses.Expense;

public class UpdateExpenseRequest : Request
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Invalid Warehouse Id")]
    public long WarehouseId { get; set; }

    [Required(ErrorMessage = "Please select one of the two expense types")]
    public CustomerGroup ExpenseType { get; set; }
    
    [Required(ErrorMessage = "Invalid Expense Category ID")]
    public long ExpenseCategoryId { get; set; }
    
    [Required(ErrorMessage = "Invalid Voucher Number")]
    [MaxLength(30,  ErrorMessage= "Max len of 30 characters")]
    public double VoucherNumber { get; set; }
    
    [Required(ErrorMessage = "Invalid Amount")]
    [MaxLength(30,  ErrorMessage= "Max len of 30 characters")]
    public double Amount { get; set; }
    
    [Required(ErrorMessage = "Please add a short not explaining the expense")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string PurchaceNote { get; set; } = String.Empty;
    
}