namespace InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;

public class GetPurchaseReturnByNumberRequest :  PagedRequest
{
    public string ReferenceNumber { get; set; } = null!;
}