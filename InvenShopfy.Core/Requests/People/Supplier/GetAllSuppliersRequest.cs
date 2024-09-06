namespace InvenShopfy.Core.Requests.People.Supplier;

public class GetAllSuppliersRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}