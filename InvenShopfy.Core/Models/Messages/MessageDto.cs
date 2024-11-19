namespace InvenShopfy.Core.Models.Messages;

public class MessageDto
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Subject { get; set; }
    public string? ToUser { get; set; }
    public string? MessageBody { get; set; }
    public DateOnly? Time { get; init; }
 
}