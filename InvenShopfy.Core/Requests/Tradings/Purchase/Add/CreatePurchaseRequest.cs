using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Tradings.Purchase.Add;

public class CreatePurchaseRequest : Request
{
    
    [Required(ErrorMessage = "Invalid WarehouseId Id")]
    public long WarehouseId { get; set; }
    
    [Required(ErrorMessage = "Invalid SupplierId Id")]
    public long SupplierId { get; set; }
    
    [Required(ErrorMessage = "Invalid SupplierId Id")]
    public long ProductId { get; set; }
    
    [Required(ErrorMessage = "Please inform the Purchase Status")]
    public EPurchaseStatus PurchaseStatus { get; set; } = EPurchaseStatus.Complete;
    
    [Required(ErrorMessage = "Invalid ShippingCost")]
    [MaxLength(20,  ErrorMessage= "Max len of 20 characters")]
    public int ShippingCost { get; set; }
    
    [Required(ErrorMessage = "Invalid PurchaseNote")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string PurchaseNote { get; set; } = null!;
}