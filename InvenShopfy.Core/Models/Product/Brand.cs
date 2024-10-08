namespace InvenShopfy.Core.Models.Product;

public class Brand
{
    public Brand()
    {
    }
    

    public Brand(long id, string title, string brandImage, string userId)
    {
        Id = id;
        Title = title;
        BrandImage = brandImage;
        UserId = userId;
    }

    public long Id { get; set; }
    public string Title { get; set; }= string.Empty;
    public string BrandImage { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
}