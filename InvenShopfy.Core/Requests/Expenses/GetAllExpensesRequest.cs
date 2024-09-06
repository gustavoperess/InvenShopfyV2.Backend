namespace InvenShopfy.Core.Requests.Expenses;

public class GetAllExpensesRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}