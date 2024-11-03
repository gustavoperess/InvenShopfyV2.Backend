using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Tradings.SalesReturn;

public class CreateSalesReturnRequest : Request
{
    
    [Required(ErrorMessage = "Please enter the Date the product was sold")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)] 
    public DateOnly ReturDate { get; set; }
    
    [Required(ErrorMessage = "Invalid Customer Id")]
    public long CustomerId { get; set; }

    public string ReferenceNumber { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid Warehouse Id")]
    public long WarehouseId { get; set; }
    
    [Required(ErrorMessage = "Invalid Biller Id")]
    public long BillerId { get; set; }

    [Required(ErrorMessage = "Invalid TotalAmount")]
    [Range(0.01, 1000000, ErrorMessage = "Total Amount must be between 0.01 and 1,000,000.")]
    public decimal TotalAmount { get; private set; } 
    
    [Required(ErrorMessage = "Please Select one of the Remarl status")]
    [AllowedValues("Duplicate", "PackageBroken", "DateExpired", "Quality", "NotGood",
        ErrorMessage = "Please select one of the allowed values Duplicate, PackageBroken, DateExpired, Quality, NotGood")]
    public string Remark { get; set; } = ERemarkStatus.NotGood.ToString();

    [MaxLength(500, ErrorMessage = "Max len of 500 characters")]
    public string ReturnNote { get; set; } = null!;
}