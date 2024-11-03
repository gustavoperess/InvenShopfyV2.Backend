namespace InvenShopfy.Core.Models.Tradings.SalesReturn;

public class SalesReturnByName
{
    public long Id { get; init; }
    public string? ReferenceNumber { get; set; }
    public string WarehouseName { get; set; } = null!;
    public string BillerName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public decimal TotalAmount { get; set; } 
    public string ReturnNote { get; set; } = null!;
    public string Remark { get; set; } = null!;
    public DateOnly ReturnDate { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
    
    
    
}