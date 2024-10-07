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
    
    
    [Required(ErrorMessage = "Please inform the Payment Status")]
    public string PaymentStatus { get; set; } = EPaymentStatus.Complete.ToString(); 
    
    [Required(ErrorMessage = "Please inform the Payment Status")]
    public double ShippingCost { get; set; } 
    
    [Required(ErrorMessage = "Please inform the Sale Status")]
    public string SaleStatus { get; set; } = ESaleStatus.Complete.ToString();
    
    [MaxLength(120, ErrorMessage = "Max len of 120 characters")]
    public string Document { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid SaleNote")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string SaleNote { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid StaffNote")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string StaffNote { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid TotalAmount")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public double TotalAmount { get; set; } 
    
    [Required(ErrorMessage = "Invalid Discount")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public int Discount { get; set; } 
    
    
    [Required(ErrorMessage = "Please enter the Date the product was sold")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)] 
    public DateOnly SaleDate { get; set; }
    
    public Dictionary<long, int> ProductIdPlusQuantity { get; set; } = new Dictionary<long, int>();
    
}