using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Tradings.Sales.SalesPayment;

public class CreateSalesPaymentRequest : Request
{
    [Required(ErrorMessage = "Please enter the Date the expense was created")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)] 
    public DateOnly Date { get; set; }
    

    [Required]
    [MaxLength(19, ErrorMessage = "Card number can't exceed 19 characters.")]
    [RegularExpression(@"^(\d{4} ){3}\d{4}$|^\d{16}$", ErrorMessage = "Invalid card number format.")]
    public string CardNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "SalesId required")]
    public long SalesId { get; set; }
    
    [Required(ErrorMessage = "Please add a short not explaining the expense")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string SalesNote { get; set; } = null!;
    
}