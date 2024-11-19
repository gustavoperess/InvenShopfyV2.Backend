namespace InvenShopfy.Core.Models.Messages;

public class Message
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string ToUserId { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly Time { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
    public bool IsImportant { get; set; } = false;
    public bool IsDeleted { get; set; }
    
    public bool IsSent { get; set; } = false;
    public bool IsReceived { get; set; } = false;
    public string UserId { get; set; } = string.Empty;
}