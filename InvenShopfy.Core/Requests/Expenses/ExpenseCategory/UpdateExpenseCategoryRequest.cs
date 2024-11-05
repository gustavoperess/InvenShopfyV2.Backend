using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Expenses.ExpenseCategory;

public class UpdateExpenseCategoryRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid Category")]
    [MaxLength(180,  ErrorMessage= "Max len of 180 characters")]
    public string Category { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Sub-Category")]
    [MaxLength(50,  ErrorMessage= "Max len of 50 characters")]
    public List<string> SubCategory { get; set; } = new List<string>();
}