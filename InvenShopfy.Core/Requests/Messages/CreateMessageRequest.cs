using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Messages;

public class CreateMessageRequest : Request
{
    [MaxLength(80, ErrorMessage = "Max len of 80 characters")]
    public string MessageTitle { get; set; } = string.Empty;

    [MaxLength(80, ErrorMessage = "Max len of 80 characters")]
    public string Subject { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "To user id is required")]
    public long ToUserId { get; set; }

    [MaxLength(500, ErrorMessage = "Max len of 80 characters")]
    public string? MessageBody { get; set; }
    
}