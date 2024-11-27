namespace InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;

public class SalesPaymentDto
{
    public long Id { get; set; }
    public string ReferenceNumber { get; init; } = null!;
    public decimal TotalAmount { get; set; }
    public string CustomerName  { get; set; } = null!;
}