namespace InvenShopfy.Core.Models.Tradings.Sales.Dto;

public sealed class BestSeller
{
    
    public long BillerId { get; set; }
    public int TotalQuantitySold { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal TotalAmount { get; set; }


}