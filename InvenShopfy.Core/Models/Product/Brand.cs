namespace InvenShopfy.Core.Models.Product;

public class Brand
{
    public long Id { get; init; }
    public string Title { get; set; }= string.Empty;
    public string BrandImage { get; set; } = null!;
    public string UserId { get; init; } = string.Empty;
}