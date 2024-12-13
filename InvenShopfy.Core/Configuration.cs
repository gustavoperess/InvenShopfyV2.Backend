namespace InvenShopfy.Core
{
    public class CloudinarySettings
    {
        public string CloudName { get; init; } = string.Empty;
        public string ApiKey { get; init; } = string.Empty;
        public string ApiSecret { get; init; } = string.Empty;
        
        public string Folder { get; init; } = string.Empty;
    }

    public class Configuration
    {
        public const int DefaultPageSize = 25;
        
        public const int DefaultPageNumber = 1;
        
        public const int DefaultStatusCode = 200;
        //delete
        public const string Deleted = "Removed Successfully";
        public const string NotDeleted = "It was not possible to delete this";

        // authorized
        public const string NotAuthorized = "User is not authorized to do this task";
        
        // Updated
        public const string Updated = "Updated Successfully";
        public const string NotUpdated = "It was not possible to update this";
        
        // Created
        public const string Created = "Created Successfully";
        public const string NotCreated = "It was not possible to create a new";
        
        // Get
        public const string Retrived = "It was not possible to retrive";
        public const string NotRetrived = "It was not possible to retrive";
        
        public const string NotFound = "Not Found";
        
        public static string ConnectionString { get; set; } = string.Empty;
        public static string BackendUrl { get; set; } = string.Empty;
        public static string FrontendUrl { get; set; } = string.Empty;
        public static string CorsPolicyName = "wam";
        public static CloudinarySettings CloudinarySettings { get; set; } = new CloudinarySettings();
        
    }
}