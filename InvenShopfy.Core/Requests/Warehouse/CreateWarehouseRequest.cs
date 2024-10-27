using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Requests.Warehouse;

public class CreateWarehouseRequest : Request
{
    
    [Required(ErrorMessage = "Invalid Warehouse Name")]
    [MaxLength(50, ErrorMessage = "Max length of 50 characters")]
    public string WarehouseName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Mobile no. is required")]
    [RegularExpression("^(?:\\+1)?\\s?\\(?\\d{3}\\)?[-.\\s]?\\d{3}[-.\\s]?\\d{4}$", ErrorMessage = "Please enter valid phone no.")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string WarehousePhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Email Address")]
    [EmailAddress]
    [MaxLength(160, ErrorMessage = "Max length of 160 characters")]
    public string WarehouseEmail { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid city name")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string WarehouseCity { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Country")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string WarehouseCountry { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Zip Code")]
    [MaxLength(20, ErrorMessage = "Max length of 20 characters")]
    public string WarehouseZipCode { get; set; } = string.Empty;
    
    [MaxLength(500, ErrorMessage = "Max length of 500 characters")]
    public string WarehouseOpeningNotes { get; set; } = string.Empty;

}