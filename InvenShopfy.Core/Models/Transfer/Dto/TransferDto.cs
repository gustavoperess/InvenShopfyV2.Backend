namespace InvenShopfy.Core.Models.Transfer.Dto;

public class TransferDto
{
    public long Id { get; set; }
    public DateOnly TransferDate { get; init; } 
    public string ReferenceNumber { get; init; } = null!;
    public int Quantity { get; set; }
    public string AuthorizedBy { get; set; }  = null!;
    public string  Reason { get; set; }  = null!;
    public string ProductName { get; set; } = null!;
    public string FromWarehouse { get; set; } = null!;
    public string ToWarehouse { get; set; }  = null!;
    public string TransferStatus { get; set; } =  null!;
    public string TransferNote { get; set; }  = null!;
}