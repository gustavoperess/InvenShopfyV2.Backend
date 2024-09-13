using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Sales;

public class Sale
{
    public long Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    private static Random random = new Random();
    public int RandomNumber { get; private set; }

    public long CustomerId { get; set; } 
    public Customer Customer { get; set; } = null!;
    
    public long WarehouseId { get; set; } 
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    
    public long BillerId { get; set; }
    public Biller Biller { get; set; } = null!;
    
    public long Product { get; set; } 
    public Product.Product ProductId { get; set; } = null!;

    public decimal ShippingCost { get; set; }
    public EPaymentStatus PaymentStatus { get; set; }
    public ESaleStatus SaleStatus { get; set; }
    public string Document { get; set; } = String.Empty;
    public string SaleNote { get; set; } = String.Empty;
    public string StafNote { get; set; } = String.Empty;
    
    public Sale()
    {
        RandomNumber = random.Next(1, 30000);
    }
    public string UserId { get; set; } = string.Empty;
}