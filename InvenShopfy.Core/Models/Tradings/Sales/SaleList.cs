using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Sales;

public class SaleList
{
    public long Id { get; set; }
    public string Customer { get; set; } = String.Empty;
    public Customer CustomerId { get; set; } = null!;
    
    public string Warehouse { get; set; } = String.Empty;
    public Warehouse.Warehouse WarehouseId { get; set; } = null!;
    
    public string Biller { get; set; } = String.Empty;
    public Biller BillerId { get; set; } = null!;
    
    public string Sales { get; set; } = String.Empty;
    public Sale SaleId { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;
}