namespace InvenShopfy.Core.Models.Messages;

public class MessageDto
{
    public long Id { get; set; }
    public string? MessageTitle { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Subject { get; set; }
    public string? ToUser { get; set; }
    public string? MessageBody { get; set; }
    public DateOnly? Time { get; init; }
 
}