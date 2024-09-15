using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Category;

public class UpdateCategoryRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid Main Category")]
    [MaxLength(50,  ErrorMessage= "Max len of 50 characters")]
    public string MainCategory { get; set; }= string.Empty;
    
    [Required(ErrorMessage = "Invalid Sub-Category")]
    [MaxLength(50,  ErrorMessage= "Max len of 50 characters")]
    public List<string> SubCategory { get; set; } = new List<string>();
}