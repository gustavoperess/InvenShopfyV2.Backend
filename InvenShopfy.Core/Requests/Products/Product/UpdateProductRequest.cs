using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Product;

public class UpdateProductRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid Title")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Amount")]
    [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Invalid Product Code")]
    [Range(1, 1000, ErrorMessage = "Product code for {0} must be between {1} and {2}.")]
    public int ProductCode { get; set; }
    
    [Required(ErrorMessage = "Invalid Category Id")]
    public long CategoryId { get; set; }
    
    [Required(ErrorMessage = "Invalid Brand Id")]
    public long BrandId { get; set; }
    
    
    [Required(ErrorMessage = "Invalid Image")]
    [Base64String]
    public string ProductImage { get; set; } = null!;
    
    public bool Featured { get; set; } = false;
    public bool DifferPriceWarehouse { get; set; } = false;
    public bool Expired { get; set; } = false;
    public bool Sale { get; set; } = false;
    
    [Required(ErrorMessage = "Invalid Unit Id")]
    public long UnitId  { get; set; }
}