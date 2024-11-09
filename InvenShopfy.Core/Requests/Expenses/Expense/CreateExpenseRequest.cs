using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Expenses.Expense;

public class CreateExpenseRequest : Request
{
    
    [Required(ErrorMessage = "Invalid Warehouse Id")]
    public long WarehouseId { get; set; }
    
    [Required(ErrorMessage = "Please enter the Date the expense was created")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)] 
    public DateOnly Date { get; set; }

    [Required(ErrorMessage = "Please select one of the two expense types")]
    [MaxLength(500, ErrorMessage = "Max len of 500 characters")]
    public string ExpenseType { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid Expense Category ID")]
    public long ExpenseCategoryId { get; set; }
    
    [Required(ErrorMessage = "Voucher number is required")]
    [Range(1, 10000000, ErrorMessage = "Voucher number for {0} must be between {1} and {2}.")]
    public long VoucherNumber { get; set; }
    
    [Required(ErrorMessage = "Invalid Amount")]
    [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000.")]
    public decimal ExpenseCost { get; set; }
    
    [Required(ErrorMessage = "Please add a short not explaining the expense")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string ExpenseNote { get; set; } = null!;
    
    [Required(ErrorMessage = "Please inform shippping cost")]
    [Range(0.01, 1000000, ErrorMessage = "Shipping Cost be between 0.01 and 1,000,000.")]
    public decimal ShippingCost { get; set; } 
    
    
    [Required(ErrorMessage = "Please Select one of the Payment status")]
    [AllowedValues("Completed", "Incompleted", "Drafts",
        ErrorMessage = "Please select one of the allowed values Completed, Incompleted, Drafts")]
    public string ExpenseStatus { get; set; } = EPaymentStatus.Completed.ToString(); 
    
    [Required(ErrorMessage = "Please add a short not explaining the description")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string ExpenseDescription { get; set; } = null!;
     
}
