using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Category;

public class UpdateCategoryRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid Title")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Title { get; set; }  = string.Empty;
    
    [Required(ErrorMessage = "Invalid Sub-Category")]
    [MaxLength(50,  ErrorMessage= "Max len of 50 characters")]
    public string SubCategory { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Main Category")]
    [MaxLength(50,  ErrorMessage= "Max len of 50 characters")]
    public string MainCategory { get; set; }= string.Empty;
}