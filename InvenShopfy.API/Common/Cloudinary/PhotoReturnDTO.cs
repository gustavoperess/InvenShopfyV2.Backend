namespace InvenShopfy.API.Common.Cloudinary;

public class PhotoReturnDTO
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; }
}