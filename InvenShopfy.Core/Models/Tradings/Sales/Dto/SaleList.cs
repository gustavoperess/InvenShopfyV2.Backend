namespace InvenShopfy.Core.Models.Tradings.Sales.Dto;

public sealed class SaleList
{
    public long Id { get; init; }
    public DateOnly SaleDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string CustomerName { get; set; }  = string.Empty;
    public string WarehouseName { get; set; }  = string.Empty;
    public string BillerName { get; set; }  = string.Empty;
    public string SaleStatus { get; set; } = string.Empty;
    public int TotalQuantitySold { get; set; }
    public decimal TotalAmount { get; set; }
    public string ReferenceNumber { get;  set; }  = string.Empty;
    public int Discount { get; set; }
    
}