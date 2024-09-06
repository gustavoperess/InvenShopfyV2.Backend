using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Unit;

public class CreateUnitRequest : Request
{
    [Required(ErrorMessage = "Invalid Title")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Title { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Short Name")]
    [MaxLength(2,  ErrorMessage= "Max len of 2 characters")]
    public string ShortName { get; set; } = String.Empty;
}