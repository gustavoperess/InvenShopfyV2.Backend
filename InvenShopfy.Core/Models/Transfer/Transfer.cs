using InvenShopfy.Core.Common.RandomNumber;
using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.Tradings.Purchase;

namespace InvenShopfy.Core.Models.Transfer;

public class Transfer
{
    public long Id { get; set; }
    public DateOnly TransferDate { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
    public string ProductName { get; set; } = null!;
    public string ReferenceNumber { get; init; } = GenerateRandomNumber.RandomNumberGenerator();
    public int Quantity { get; set; }
    public string AuthorizedBy { get; set; }  = null!;
    public string  Reason { get; set; }  = null!;
    public long FromWarehouseId { get; set; }
    public long ToWarehouseId { get; set; } 
    public string TransferStatus { get; set; } = ETransferStatus.Pending.ToString();
    public string TransferNote { get; set; }  = null!;
    public string UserId { get; init; } = string.Empty;
}