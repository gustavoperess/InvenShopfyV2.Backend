namespace InvenShopfy.Core.Models.People.Dto;

public class TopSupplier
{
    public long Id { get; init; }
    public string Name { get; set; } = String.Empty;
    public long SupplierCode { get; set; }

    public decimal TotalPurchase { get; set; }
    public string Company { get; set; } = String.Empty;
}