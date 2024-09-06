namespace InvenShopfy.Core.Requests.People.Biller;

public class GetAllBillerRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}