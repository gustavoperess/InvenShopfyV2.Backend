using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Expenses.Expense;

public class CreateExpensePaymentRequest : Request
{
    
    [Required(ErrorMessage = "Please enter the Date the expense was created")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)] 
    public DateOnly Date { get; set; }

    [MaxLength(500, ErrorMessage = "Max len of 500 characters")]
    public string CardNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "ExpenseId required")]
    public long ExpenseId { get; set; }
    
    [Required(ErrorMessage = "Please add a short not explaining the expense")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string ExpenseNote { get; set; } = null!;
    
    [Required(ErrorMessage = "Please Select one of the Payment status")]
    [AllowedValues("Cash", "Card",ErrorMessage = "Please select one of the allowed values Cash, Card")]
    public string PaymentType { get; set; } = EPaymentType.Cash.ToString(); 
    
    
}