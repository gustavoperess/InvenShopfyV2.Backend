using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;

public class UpdatePurchaseRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid WarehouseId Id")]
    public long WarehouseId { get; set; }
    
    [Required(ErrorMessage = "Invalid SupplierId Id")]
    public long SupplierId { get; set; }
    
    [Required(ErrorMessage = "Please inform the Purchase Status")]
    public string PurchaseStatus { get; set; } = EStatus.Completed.ToString();
    
    [Required(ErrorMessage = "Please inform total amount bought")]
    [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000.")]
    public decimal TotalAmountBought { get; set; }
    
    [Required(ErrorMessage = "Invalid ShippingCost")]
    [Range(0.01, 1000000, ErrorMessage = "Shipping Cost must be between 0.01 and 1,000,000.")]
    public decimal ShippingCost { get; set; } 
    
    [Required(ErrorMessage = "Invalid PurchaseNote")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string PurchaseNote { get; set; } = null!;
}