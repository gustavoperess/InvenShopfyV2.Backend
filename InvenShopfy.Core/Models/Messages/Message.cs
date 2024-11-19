namespace InvenShopfy.Core.Models.Messages;

public class Message
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Subject { get; set; }
    public long ToUserId { get; set; }
    public string? MessageBody { get; set; }
    public DateOnly Time { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
    public bool IsImportant { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsSent { get; set; }
    public bool IsReceived { get; set; }
    public string UserId { get; set; } = string.Empty;
}