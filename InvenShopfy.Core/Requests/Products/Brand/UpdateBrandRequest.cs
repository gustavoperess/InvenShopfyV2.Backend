using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Brand;

public class UpdateBrandRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid Transaction")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string BrandName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Image")]
    [Base64String]
    public string BrandImage { get; set; } = string.Empty;
    
    
  
}