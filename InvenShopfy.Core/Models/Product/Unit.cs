namespace InvenShopfy.Core.Models.Product;

public class Unit
{
    public long Id { get; init; }
    public string UnitName { get; set; } = String.Empty;
    public string UnitShortName { get; set; } = String.Empty;
    public string UserId { get; init; } = string.Empty;
}