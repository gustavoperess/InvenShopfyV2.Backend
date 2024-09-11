using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Sales;

public class Sale
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Customer { get; set; } = String.Empty;
    public Customer CustomerId { get; set; } = null!;
    
    public string Warehouse { get; set; } = String.Empty;
    public Warehouse.Warehouse WarehouseId { get; set; } = null!;
    
    public string Biller { get; set; } = String.Empty;
    public Biller BillerId { get; set; } = null!;
    
    public string Product { get; set; } = String.Empty;
    public Product.Product ProductId { get; set; } = null!;

    public int ShippingCost { get; set; }
    public EPaymentStatus PaymentStatus { get; set; }
    public ESaleStatus SaleStatus { get; set; }
    public string Document { get; set; } = String.Empty;
    public string SalesNote { get; set; } = String.Empty;
    public string StafNote { get; set; } = String.Empty;
    
}
