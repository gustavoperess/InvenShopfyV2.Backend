using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.SalesReturn;

public class SaleReturn
{
    public long Id { get; init; }
    public string ReferenceNumber { get; set; } = null!;
    
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; init; } = null!;
    public long BillerId { get; set; }
    public Biller Biller { get; init; } = null!;
    public long CustomerId { get; set; }
    public Customer Customer { get; init; } = null!;
    public decimal TotalAmount { get; set; } 
    public string ReturnNote { get; set; } = null!;
    public string RemarkStatus { get; set; } = null!;
    public DateOnly ReturnDate { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
    public string UserId { get; init; } = string.Empty;
}