namespace InvenShopfy.Core.Models.Tradings.Returns.SalesReturn.Dto;

public sealed class SalesReturnByReturnNumber
{
    public long Id { get; init; }
    public string? ReferenceNumber { get; set; }
    public string WarehouseName { get; set; } = null!;
    public string BillerName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public decimal TotalAmount { get; set; } 
    
}