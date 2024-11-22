namespace InvenShopfy.Core.Requests.Notifications;

public class CreateNotificationsRequest : Request
{
    public string NotificationTitle { get; set; } = string.Empty;
    public string? Image { get; set; }
    public bool Urgency { get; set; }
    public string From { get; set; } = string.Empty;
    public string Href { get; set; } = null!;

}