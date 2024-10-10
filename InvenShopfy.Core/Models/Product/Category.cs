namespace InvenShopfy.Core.Models.Product;

public class Category
{

    public long Id { get; set; }
    public string MainCategory { get; set; }  = string.Empty;
    public List<string> SubCategory { get; set; } = new List<string>();
    public string UserId { get; init; } = string.Empty;
    
}