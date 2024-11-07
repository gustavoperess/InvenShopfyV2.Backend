namespace InvenShopfy.Core.Requests.Warehouse;

public class GetTotalQuantityByWarehouseAndProductIdRequest
{
    public long ProductId { get; set; }
    public long WarehouseId { get; set; }
    
}