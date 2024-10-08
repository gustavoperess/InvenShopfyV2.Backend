namespace InvenShopfy.Core.Models.Product;

public class Category
{
    public Category()
    {
    }
    
    public Category(long id, string mainCategory, List<string> subCategory, string userId)
    {
        Id = id;
        MainCategory = mainCategory;
        SubCategory = subCategory;
        UserId = userId;
    }

    public long Id { get; set; }
    public string MainCategory { get; set; }  = string.Empty;
    public List<string> SubCategory { get; set; } = new List<string>();
    public string UserId { get; set; } = string.Empty;
    
}