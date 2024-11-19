using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Messages;

public class CreateMessageRequest : Request
{
    [MaxLength(80, ErrorMessage = "Max len of 80 characters")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(80, ErrorMessage = "Max len of 80 characters")]
    public string Subtitle { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "To user id is required")]
    public string ToUserId { get; set; } = null!;

    [MaxLength(500, ErrorMessage = "Max len of 80 characters")]
    public string Description { get; set; } = null!;

    public bool IsImportant { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
}