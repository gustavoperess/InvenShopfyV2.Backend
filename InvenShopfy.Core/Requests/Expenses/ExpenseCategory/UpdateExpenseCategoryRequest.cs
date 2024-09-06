using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Expenses.ExpenseCategory;

public class UpdateExpenseCategoryRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Invalid Category")]
    [MaxLength(180,  ErrorMessage= "Max len of 180 characters")]
    public string Category { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Subcategory")]
    [MaxLength(180,  ErrorMessage= "Max len of 180 characters")]
    public string SubCategory { get; set; } = String.Empty;
}