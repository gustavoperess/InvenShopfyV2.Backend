namespace InvenShopfy.Core.Models.Tradings.Sales;

public class MostSoldProduct
{
    public string ProductName { get; set; } = string.Empty;
    public int TotalQuantitySoldPerProduct { get; set; } 
    public double TotalPricePerProduct { get; set; }
    
    public int ProductCode { get; set; }
}