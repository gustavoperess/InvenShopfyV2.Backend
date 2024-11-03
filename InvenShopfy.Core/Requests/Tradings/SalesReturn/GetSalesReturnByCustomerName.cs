namespace InvenShopfy.Core.Requests.Tradings.SalesReturn;

public class GetSalesReturnByCustomerName : Request
{
    public string CustomerName { get; set; } = null!;
}