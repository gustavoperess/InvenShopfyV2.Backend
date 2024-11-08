namespace InvenShopfy.Core.Models.Tradings.Purchase.Dto;

public class PurchaseDashboard
{
    public long Id { get; init; }
    
    public DateOnly PurchaseDate { get; init; } 
    
    public string Supplier { get; init; } = null!;
    
    public string PurchaseStatus { get; set; }  = null!;
    
    public string ReferenceNumber { get; set; }   = null!;
    
    public decimal TotalAmountBought { get; set; }
    
    public int TotalNumberOfProductsBought { get; set; }

}