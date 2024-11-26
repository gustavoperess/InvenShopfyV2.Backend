using InvenShopfy.Core.Common.RandomNumber;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.Transfer;

public class Transfer
{
    public long Id { get; set; }
    public DateOnly TransferDate { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
    public string ReferenceNumber { get; init; } = GenerateRandomNumber.RandomNumberGenerator();
    public int Quantity { get; set; }
    public string AuthorizedBy { get; set; }  = null!;
    public string  Reason { get; set; }  = null!;
    public long ProductId { get; set; }
    public Product.Product Product { get; set; } = null!;
    public long FromWarehouseId { get; set; }
    public Warehouse.Warehouse FromWarehouse { get; set; } = null!;
    public long ToWarehouseId { get; set; } 
    public Warehouse.Warehouse ToWarehouse { get; set; } = null!;
    public string TransferStatus { get; set; } = EStatus.Pending.ToString();
    public string TransferNote { get; set; }  = null!;
    public string UserId { get; init; } = string.Empty;
}