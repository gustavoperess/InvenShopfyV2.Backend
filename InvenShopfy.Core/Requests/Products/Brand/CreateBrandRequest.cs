using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Brand;

public class CreateBrandRequest : Request
{
    [Required(ErrorMessage = "Invalid Title")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Title { get; set; }= string.Empty;
    
    [Required(ErrorMessage = "Invalid Image")]
    [Base64String]
    public string BrandImage { get; set; } = null!;
    
}