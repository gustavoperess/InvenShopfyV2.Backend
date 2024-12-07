namespace InvenShopfy.Core.Models.Notifications;

public class Notification
{
    public long Id { get; set; }
    public bool Urgency { get; set; }
    public string? Image { get; set; }
    public string NotificationTitle { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public DateTime CreateAt { get; init; }
    public string Href { get; set; } = null!;
    
}