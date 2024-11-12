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
    [Range(0.01, 1000000, ErrorMessage = "Shipping Cost be between 0.01 and 1,000,000.")]
    public decimal ShippingCost { get; set; }

    public int? ProfitLine { get; set; } = 0;

    public int? TaxAmount { get; set; }
    
    [Required(ErrorMessage = "Please Select one of the Sale status")]
    [AllowedValues("Completed", "Incompleted",
        ErrorMessage = "Please select one of the allowed values Completed, Incompleted, Drafts")]
    public string SaleStatus { get; set; } = ESaleStatus.Completed.ToString();

    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string SaleNote { get; set; } = null!;
    
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string StaffNote { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid TotalAmount")]
    [Range(0.01, 1000000, ErrorMessage = "Total Amount must be between 0.01 and 1,000,000.")]
    public decimal TotalAmount { get; set; } 
    
    [Required(ErrorMessage = "Invalid Discount")]
    [Range(0.01, 1000000, ErrorMessage = "Total Discount must be between 0.01 and 1,000,000.")]
    public int Discount { get; set; } 
    
    
    [Required(ErrorMessage = "Please enter the Date the product was sold")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)] 
    public DateOnly SaleDate { get; set; }
    public Dictionary<long, int> ProductIdPlusQuantity { get; set; } = new Dictionary<long, int>();
    
}