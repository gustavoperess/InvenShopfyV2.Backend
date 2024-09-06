namespace InvenShopfy.Core.Requests.Expenses;

public class GetExpenseByIdRequest : Request
{
    public long Id { get; set; }
}