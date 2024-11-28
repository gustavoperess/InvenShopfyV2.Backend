namespace InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;

public class SalesPaymentDto
{
    public long Id { get; set; }
    public string ReferenceNumber { get; init; } = null!;
    public decimal TotalAmount { get; set; }

    public string CardNumber { get; set; }  = null!;
    public string WarehouseName { get; set; } = null!;
    public string CustomerName  { get; set; } = null!;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string PaymentType { get; set; } = null!;
}