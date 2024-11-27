using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;

public class SalesPayment
{
    public long Id { get; set; }
    public long SalesId { get; set; }
    public Sale Sales { get; init; } = null!;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string PaymentType { get; set; }  = EPaymentType.Cash.ToString();
    public string CardNumber { get; set; } = string.Empty;
    public string SalesNote { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}