namespace InvenShopfy.Core.Requests;

public class Request
{
    public string UserId { get; set; } = string.Empty;
    public bool UserHasPermission { get; set; }
}