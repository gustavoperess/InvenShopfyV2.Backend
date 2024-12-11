using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;

public class CreateSalesReturnRequest : Request
{
    
    [Required(ErrorMessage = "Please enter the Date the sale was returned")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)] 
    public DateOnly ReturnDate { get; set; }
    
    [Required(ErrorMessage = "Invalid Customer name")]
    public string CustomerName { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid Reference Number")]
    public string ReferenceNumber { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid Warehouse Name")]
    public string WarehouseName { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid Sale Id")]
    
    public string BillerName { get; set; } = null!;

    [Required(ErrorMessage = "Invalid TotalAmount")]
    [Range(0.01, 1000000, ErrorMessage = "Total Amount must be between 0.01 and 1,000,000.")]
    public decimal ReturnTotalAmount { get; set; } 
    
    [Required(ErrorMessage = "Please Select one of the Remarl status")]
    [AllowedValues("Duplicated", "PackageBroken", "DateExpired", "Quality", "NotGood",
        ErrorMessage = "Please select one of the allowed values Duplicated, PackageBroken, DateExpired, Quality, NotGood")]
    
    public string RemarkStatus { get; set; } = null!;

    [MaxLength(500, ErrorMessage = "Max len of 500 characters")]
    public string ReturnNote { get; set; } = null!;
}