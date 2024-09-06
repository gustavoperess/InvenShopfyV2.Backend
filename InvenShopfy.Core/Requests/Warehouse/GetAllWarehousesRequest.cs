namespace InvenShopfy.Core.Requests.Warehouse;

public class GetAllWarehousesRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}