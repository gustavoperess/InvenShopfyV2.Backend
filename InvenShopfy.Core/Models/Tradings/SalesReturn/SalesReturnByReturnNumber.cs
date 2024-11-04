namespace InvenShopfy.Core.Models.Tradings.SalesReturn;

public class SalesReturnByReturnNumber
{
    public long Id { get; init; }
    public string? ReferenceNumber { get; set; }
    public string WarehouseName { get; set; } = null!;
    public string BillerName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public decimal TotalAmount { get; set; } 
    
}