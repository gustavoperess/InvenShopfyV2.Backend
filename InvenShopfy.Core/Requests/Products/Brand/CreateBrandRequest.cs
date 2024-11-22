using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Brand;

public class CreateBrandRequest : Request
{
    [Required(ErrorMessage = "Invalid BrandName")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string BrandName { get; set; }= string.Empty;
    
    [Base64String]
    public string? BrandImage { get; set; }
    
}