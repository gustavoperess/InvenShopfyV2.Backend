namespace InvenShopfy.Core.Models.Notifications;

public class Notification
{
    public long Id { get; set; }
    public bool Urgency { get; set; }
    public string? Image { get; set; }
    public string NotificationTitle { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public DateOnly CreateAt { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    public string Href { get; set; } = null!;
    
}