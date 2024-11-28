namespace InvenShopfy.API.EndPoints.Identity.Role;

public class Permissions
{
    public Dictionary<string, bool> Product { get; set; } = new Dictionary<string, bool>
    {
        {"add", true },
        {"view", true },
        {"edit", true },
        {"delete", true },
    };
    
    public Dictionary<string, bool> Sale { get; set; } = new Dictionary<string, bool>
    {
        {"create", true },
        {"view", true },
        {"edit", false },
        {"delete", false }
    };

    // Permissions for Staff management
    public Dictionary<string, bool> Staff { get; set; } = new Dictionary<string, bool>
    {
        {"add", false },
        {"view", true },
        {"edit", true },
        {"delete", false }
    };
}