namespace InvenShopfy.Core.Requests.UserManagement.User;

public class GetAllUsersRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}