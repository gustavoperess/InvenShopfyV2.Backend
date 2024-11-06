using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Models.Warehouse;

public class Warehouse
{
    public long Id { get; init; }
    public string WarehouseName { get; set; } = string.Empty;
    public string WarehousePhoneNumber { get; set; } = string.Empty;
    public string WarehouseEmail { get; set; } = string.Empty;
    public string WarehouseCity { get; set; } = string.Empty;
    public string WarehouseCountry { get; set; } = string.Empty;
    public string WarehouseZipCode { get; set; } = string.Empty;
    public string WarehouseOpeningNotes { get; set; } = string.Empty;
    public int QuantityOfItems { get; set; }
    public string UserId { get; init; } = string.Empty;
}