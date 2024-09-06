namespace InvenShopfy.Core.Requests.UserManagement.Role;

public class GetAllRolesRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}