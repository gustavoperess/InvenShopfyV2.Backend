using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Product;

public class UpdateProductRequest : Request
{
    public long Id { get; set; }
    
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string? ProductName { get; set; }
    
    [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000")]
    public decimal? ProductPrice { get; set; }
    
    public long? CategoryId { get; set; }
    
    public long? BrandId { get; set; }
    
    [Range(1, 14, ErrorMessage = "Product code for {0} must be between {1} and {2}.")]
    public int? TaxPercentage { get; set; }
    
    public string? MarginRange { get; set; }
    
    [Base64String]
    public string? ProductImage { get; set; } 
    
    public string? SubCategory { get; set; }
    
    public long? UnitId  { get; set; }
}