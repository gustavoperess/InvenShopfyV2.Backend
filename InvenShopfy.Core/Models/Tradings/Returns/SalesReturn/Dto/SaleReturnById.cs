namespace InvenShopfy.Core.Models.Tradings.Returns.SalesReturn.Dto;

public class SaleReturnById
{
    public long Id { get; init; }
    public string ReferenceNumber { get; set; } = null!;
    public string BillerName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string WarehouseName { get; set; } = null!;
    public decimal ReturnTotalAmount { get; set; } 
    public string ReturnNote { get; set; } = null!;
    public string RemarkStatus { get; set; } = null!;
    public DateOnly ReturnDate { get; init; } 
}