namespace InvenShopfy.Core.Models.Product;

public class Category
{
    public long Id { get; set; }
    public string Title { get; set; }  = string.Empty;
    public string SubCategory { get; set; } = string.Empty;
    public string MainCategory { get; set; }= string.Empty;
    public string UserId { get; set; } = string.Empty;
    
}