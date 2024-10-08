namespace InvenShopfy.Core.Models.Product;

public class Unit
{
    public Unit()
    {
    }
    

    public Unit(long id, string title, string shortName, string userId)
    {
        Id = id;
        Title = title;
        ShortName = shortName;
        UserId = userId;
    }

    public long Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string ShortName { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
}