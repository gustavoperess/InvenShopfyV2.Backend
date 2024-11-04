namespace InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn;

public class PurchaseReturn
{
    public long Id { get; init; }
    public string ReferenceNumber { get; set; } = null!;
    public string SupplierName { get; set; } = null!;
    public string WarehouseName { get; set; } = null!;
    public decimal TotalAmount { get; set; } 
    public string ReturnNote { get; set; } = null!;
    public string RemarkStatus { get; set; } = null!;
    public DateOnly ReturnDate { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
    public string UserId { get; init; } = string.Empty;
}