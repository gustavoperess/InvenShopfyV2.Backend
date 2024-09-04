namespace InvenShopfy.Core;

public class Configuration
{
    public const int DefaultPageSize = 25;
    public const int DefaultPageNumber = 1;
    public const int DefaultStatusCode = 200;
    public static string ConnectionString { get; set; } = string.Empty;
}