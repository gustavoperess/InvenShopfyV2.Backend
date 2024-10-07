using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Sales;

public class SaleList
{
    public long Id { get; set; }
    public DateOnly SaleDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string CustomerName { get; set; }  = string.Empty;
    public string WarehouseName { get; set; }  = string.Empty;
    public string BillerName { get; set; }  = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string SaleStatus { get; set; } = string.Empty;
    public int TotalQuantitySold { get; set; }
    public decimal TotalAmount { get; set; }
    public string ReferenceNumber { get;  set; }  = string.Empty;
    public int Discount { get; set; }
    
}