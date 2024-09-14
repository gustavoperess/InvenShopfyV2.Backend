using InvenShopfy.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Tradings.Sales;

public class CreateSalesRequest : Request
{
    [Required(ErrorMessage = "Invalid Customer Id")]
    public long CustomerId { get; set; }
    
    [Required(ErrorMessage = "Invalid Warehouse Id")]
    public long WarehouseId { get; set; }
    
    [Required(ErrorMessage = "Invalid Biller Id")]
    public long BillerId { get; set; }
    
    [Required(ErrorMessage = "Invalid Product Id")]
    public long ProductId { get; set; }
    
    [Required(ErrorMessage = "Please inform the Payment Status")]
    public EPaymentStatus PaymentStatus { get; set; } = EPaymentStatus.Complete;
    
    [Required(ErrorMessage = "Please inform the Payment Status")]
    public double ShippingCost { get; set; } 
    
    [Required(ErrorMessage = "Please inform the Payment Status")]
    public int RandomNumber { get; set; } 
    
    [Required(ErrorMessage = "Please inform the Sale Status")]
    public ESaleStatus SaleStatus { get; set; } = ESaleStatus.Complete;

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