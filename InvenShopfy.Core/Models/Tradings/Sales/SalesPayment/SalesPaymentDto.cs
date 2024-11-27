namespace InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;

public class SalesPaymentDto
{
    public long Id { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string VoucherNumber { get; init; } = null!;
    public decimal ExpenseCost { get; set; }
    public string ExpenseCategory  { get; set; } = null!;
    public string PaymentType { get; set; } =  null!;
    public string CardNumber { get; set; } = null!;
}