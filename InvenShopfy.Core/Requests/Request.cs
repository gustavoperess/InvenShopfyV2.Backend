namespace InvenShopfy.Core.Requests;

public class Request
{
    public string UserId { get; set; } = string.Empty;
    public List<long> RoleIds { get; set; } = new List<long>();

    // public List<string> RoleNames { get; set; } = new List<string>();
}