namespace InvenShopfy.Core.Requests.Tradings.SalesReturn;

public class GetSalesReturnByNumberRequest : PagedRequest
{
    public string ReferenceNumber { get; set; } = null!;
}