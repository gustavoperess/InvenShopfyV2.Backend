using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Products.Unit;

public class CreateUnitRequest : Request
{
    [Required(ErrorMessage = "Invalid UnitName")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string UnitName { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Short Name")]
    [MaxLength(2,  ErrorMessage= "Max len of 2 characters")]
    public string UnitShortName { get; set; } = String.Empty;
}