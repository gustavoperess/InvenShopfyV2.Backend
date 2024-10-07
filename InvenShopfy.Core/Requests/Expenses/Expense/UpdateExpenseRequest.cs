using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.Expenses;

namespace InvenShopfy.Core.Requests.Expenses.Expense;

public class UpdateExpenseRequest : Request
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Invalid Warehouse Id")]
    public long WarehouseId { get; set; }

    [Required(ErrorMessage = "Please select one of the two expense types")]
    [AllowedValues("DraftExpense", "DirectExpense",
        ErrorMessage = "Please select one of the allowed values are DraftExpense, DirectExpense")]
    public string ExpenseType { get; set; } = EExpenseType.DirectExpense.ToString();
    
    [Required(ErrorMessage = "Invalid Expense Category ID")]
    public long ExpenseCategoryId { get; set; }
    
    [Required(ErrorMessage = "Voucher number is required")]
    [Range(1, 1000, ErrorMessage = "Voucher number for {0} must be between {1} and {2}.")]
    public long VoucherNumber { get; set; }
    
    [Required(ErrorMessage = "Invalid Amount")]
    [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000.")]
    public decimal Amount { get; set; }
    
    [Required(ErrorMessage = "Please add a short not explaining the expense")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string PurchaceNote { get; set; } = String.Empty;
    
}