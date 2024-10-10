namespace InvenShopfy.Core.Models.Product;

public class Unit
{
    public long Id { get; init; }
    public string Title { get; set; } = String.Empty;
    public string ShortName { get; set; } = String.Empty;
    public string UserId { get; init; } = string.Empty;
}