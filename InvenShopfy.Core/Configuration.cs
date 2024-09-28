namespace InvenShopfy.Core
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
        public string Folder { get; set; } = string.Empty;
    }

    public class Configuration
    {
        public const int DefaultPageSize = 25;
        public const int DefaultPageNumber = 1;
        public const int DefaultStatusCode = 200;
        public static string ConnectionString { get; set; } = string.Empty;
        public static string BackendUrl { get; set; } = string.Empty;
        public static string FrontendUrl { get; set; } = string.Empty;
        public static string CorsPolicyName = "wam";
        public static CloudinarySettings CloudinarySettings { get; set; } = new CloudinarySettings();
        
    }
}