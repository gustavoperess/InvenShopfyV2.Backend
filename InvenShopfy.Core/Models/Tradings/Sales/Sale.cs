using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Sales;

public class Sale
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Customer { get; set; }
    public Customer CustomerId { get; set; }
}