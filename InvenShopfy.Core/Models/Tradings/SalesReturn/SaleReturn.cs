using System.Text.Json.Serialization;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.Tradings.Sales;

namespace InvenShopfy.Core.Models.Tradings.SalesReturn;

public class SaleReturn
{
    public long Id { get; init; }
    public string ReferenceNumber { get; set; } = null!;
    public string BillerName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string WarehouseName { get; set; } = null!;
    public decimal TotalAmount { get; set; } 
    public string ReturnNote { get; set; } = null!;
    public string RemarkStatus { get; set; } = null!;
    public DateOnly ReturnDate { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
    public string UserId { get; init; } = string.Empty;
}