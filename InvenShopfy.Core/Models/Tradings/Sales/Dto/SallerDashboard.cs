namespace InvenShopfy.Core.Models.Tradings.Sales.Dto;

public class SallerDashboard
{
    public long Id { get; init; }
    public DateOnly SaleDate { get; set; }
    public string Customer { get; init; } = null!;
    public string PaymentStatus { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
    public int TotalQuantitySold { get; set; }
    public string ReferenceNumber { get; init; } = null!;
    
}