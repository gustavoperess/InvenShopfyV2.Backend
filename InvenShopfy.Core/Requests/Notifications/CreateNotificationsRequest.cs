namespace InvenShopfy.Core.Requests.Notifications;

public class CreateNotificationsRequest : Request
{
    public string Title { get; set; } = string.Empty;
    public string? Image { get; set; }
    public bool Urgency { get; set; }
    public string From { get; set; } = string.Empty;
    public string Href { get; set; } = null!;

}