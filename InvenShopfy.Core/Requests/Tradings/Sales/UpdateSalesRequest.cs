using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Tradings.Sales;

public class UpdateSalesRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid Customer Id")]
    public long CustomerId { get; set; }
    
    [Required(ErrorMessage = "Invalid Warehouse Id")]
    public long WarehouseId { get; set; }
    
    [Required(ErrorMessage = "Invalid Biller Id")]
    public long BillerId { get; set; }
    
    [Required(ErrorMessage = "Invalid Product Id")]
    public long ProductId { get; set; }
    
    [Required(ErrorMessage = "Please Select one of the Payment status")]
    [AllowedValues("Complete", "Incomplete", "Drafts",
        ErrorMessage = "Please select one of the allowed values Complete, Incomplete, Drafts")]
    public string PaymentStatus { get; set; } = EPaymentStatus.Complete.ToString(); 
    
    [Required(ErrorMessage = "Please inform the Payment Status")]
    [Range(0.01, 1000000, ErrorMessage = "Shipping Cost be between 0.01 and 1,000,000.")]
    public decimal ShippingCost { get; set; } 
    
    
    [Required(ErrorMessage = "Please Select one of the Sale status")]
    [AllowedValues("Complete", "Incomplete", "Drafts",
        ErrorMessage = "Please select one of the allowed values Complete, Incomplete, Drafts")]
    public string SaleStatus { get; set; } = ESaleStatus.Complete.ToString();

    [Required(ErrorMessage = "Invalid Document")]
    [MaxLength(120, ErrorMessage = "Max len of 120 characters")]
    public string Document { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid SaleNote")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string SaleNote { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid StafNote")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string StafNote { get; set; } = null!;
}