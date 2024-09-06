namespace InvenShopfy.Core.Requests.People.Customer;

public class GetAllCustomersRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}