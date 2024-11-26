namespace InvenShopfy.Core.Models.Tradings.Returns.SalesReturn.Dto;

public class SalesReturnDashboard
{
    public long Id { get; init; }
    public string ReferenceNumber { get; set; } = null!;
    public string BillerName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public decimal ReturnTotalAmount { get; set; } 
    public string RemarkStatus { get; set; } = null!;
    public DateOnly ReturnDate { get; init; } 
}