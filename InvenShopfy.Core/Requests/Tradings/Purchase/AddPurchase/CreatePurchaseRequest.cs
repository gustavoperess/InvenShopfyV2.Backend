using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;

public class CreatePurchaseRequest : Request
{
    
    [Required(ErrorMessage = "Invalid WarehouseId Id")]
    public long WarehouseId { get; set; } // warehouse the product is 
    
    [Required(ErrorMessage = "Invalid SupplierId Id")]
    public long SupplierId { get; set; } // who I bought it from 
    
    [Required(ErrorMessage = "Invalid SupplierId Id")]
    public long ProductId { get; set; } // the product ID.
    
    
    [Required(ErrorMessage = "Please inform the Purchase Status")]
    public int Quantity { get; set; }
    
    [Required(ErrorMessage = "Please inform the Purchase Status")]
    public string PurchaseStatus { get; set; } = EPurchaseStatus.Complete.ToString();
    
    [Required(ErrorMessage = "Invalid ShippingCost")]
    [MaxLength(20,  ErrorMessage= "Max len of 20 characters")]
    public int ShippingCost { get; set; } 
    
    [Required(ErrorMessage = "Invalid PurchaseNote")]
    [MaxLength(500,  ErrorMessage= "Max len of 500 characters")]
    public string PurchaseNote { get; set; } = null!;
}