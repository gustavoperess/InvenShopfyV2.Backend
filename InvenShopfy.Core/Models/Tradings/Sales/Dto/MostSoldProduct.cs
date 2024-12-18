namespace InvenShopfy.Core.Models.Tradings.Sales.Dto;

public sealed class MostSoldProduct
{
    public long Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int TotalQuantitySoldPerProduct { get; set; } 
    public decimal TotalAmountSold { get; set; } 

    public int ProductCode { get; set; }
}