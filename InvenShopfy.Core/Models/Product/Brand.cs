namespace InvenShopfy.Core.Models.Product;

public class Brand
{
    public long Id { get; set; }
    public string Title { get; set; }= string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string BrandImage { get; set; } = null!;
}