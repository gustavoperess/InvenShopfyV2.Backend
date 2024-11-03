namespace InvenShopfy.Core.Requests.Tradings.SalesReturn;

public class GetSalesReturnByNumberRequest : PagedRequest
{
    public string ReturNumber { get; set; } = null!;
}