using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Product;

public class UpdateProductRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid Title")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Amount")]
    public double Price { get; set; }
    
    [Required(ErrorMessage = "Invalid Product Code")]
    [MaxLength(20,  ErrorMessage= "Max len of 20 characters")]
    public int ProductCode { get; set; }
    
    [Required(ErrorMessage = "Invalid Category Id")]
    public long CategoryId { get; set; }
    
    [Required(ErrorMessage = "Invalid Brand Id")]
    public long BrandId { get; set; }
    
    // [Required(ErrorMessage = "Invalid Image")] // NEED TO COME BACK AS I WILL BE ADDING THIS TO CLOUDNIARY
    public string ProductImage { get; set; } = null!;
    
    [Required(ErrorMessage = "Invalid Unit Id")]
    public long UnitId  { get; set; }
}