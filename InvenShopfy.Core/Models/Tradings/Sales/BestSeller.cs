using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Sales;

public class BestSeller
{
    
    public long BillerId { get; set; }
    public int TotalQuantitySold { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal TotalAmount { get; set; }


}