namespace InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;

public class GetSalesReturnByNumberRequest : PagedRequest
{
    public string ReferenceNumber { get; set; } = null!;
}