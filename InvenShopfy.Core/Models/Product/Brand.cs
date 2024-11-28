namespace InvenShopfy.Core.Models.Product;

public class Brand
{
    public long Id { get; init; }
    public string BrandName { get; set; }= string.Empty;
    public string BrandImage { get; set; } = null!;

}