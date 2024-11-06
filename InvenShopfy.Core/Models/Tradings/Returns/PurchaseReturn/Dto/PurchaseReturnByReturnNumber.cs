namespace InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn.Dto;

public sealed class PurchaseReturnByReturnNumber
{
    public long Id { get; init; }
    public string? ReferenceNumber { get; set; }
    public string WarehouseName { get; set; } = null!;
    public string SupplierName { get; set; } = null!;
    public decimal TotalAmount { get; set; } 
}