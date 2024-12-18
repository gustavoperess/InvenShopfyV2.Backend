using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Requests.Warehouse;

public class UpdateWarehouseRequest : Request
{
    public long Id { get; set; }

    [MaxLength(50, ErrorMessage = "Max length of 50 characters")]
    public string WarehouseName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^\(\d{3}\)\s\d{3}-\d{4}$", ErrorMessage = "Please enter a valid phone number in the format (123) 456-7890.")]
    [MaxLength(16, ErrorMessage = "Max length of 16 characters")]
    public string WarehousePhoneNumber { get; set; } = string.Empty;
    
    [EmailAddress]
    [MaxLength(160, ErrorMessage = "Max length of 160 characters")]
    public string WarehouseEmail { get; set; } = string.Empty;
    
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string WarehouseCity { get; set; } = string.Empty;
    
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string WarehouseCountry { get; set; } = string.Empty;
    
    [MaxLength(20, ErrorMessage = "Max length of 20 characters")]
    public string WarehouseZipCode { get; set; } = string.Empty;
    
    [MaxLength(500, ErrorMessage = "Max length of 500 characters")]
    public string WarehouseOpeningNotes { get; set; } = string.Empty;
}