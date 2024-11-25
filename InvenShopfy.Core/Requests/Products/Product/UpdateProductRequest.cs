using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Product;

public class UpdateProductRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid ProductName")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string ProductName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Amount")]
    [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000")]
    public decimal ProductPrice { get; set; }
    
    [Required(ErrorMessage = "Invalid Product Code")]
    [Range(1, 10000000, ErrorMessage = "Product code for {0} must be between {1} and {2}.")]
    public int ProductCode { get; set; }
    
    [Required(ErrorMessage = "Invalid Category Id")]
    public long CategoryId { get; set; }
    
    [Required(ErrorMessage = "Invalid Brand Id")]
    public long BrandId { get; set; }
    
    [Required(ErrorMessage = "Invalid tax amount")]
    [Range(1, 14, ErrorMessage = "Product code for {0} must be between {1} and {2}.")]
    public int TaxPercentage { get; set; }
    
    [Required(ErrorMessage = "Please enter margin range")]
    public string MarginRange { get; set; } = null!;
    
    [Base64String]
    public string? ProductImage { get; set; } 
    
    [Required(ErrorMessage = "Invalid SubCategory")]
    public string Subcategory { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Unit Id")]
    public long UnitId  { get; set; }
}