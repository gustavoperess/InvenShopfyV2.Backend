using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Expenses;

public class CreateExpenseRequest : Request
{
    
    [Required(ErrorMessage = "Invalid Warehouse Id")]
    public long WarehouseId { get; set; }

    [Required(ErrorMessage = "Please select one of the two expenses below")]
    public List<string> ExpenseType { get; set; } = new List<string> { "Direct Expense", "Draft Expense" };
    
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
