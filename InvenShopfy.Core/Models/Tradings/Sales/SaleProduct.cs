using System.Text.Json.Serialization;

namespace InvenShopfy.Core.Models.Tradings.Sales;

public class SaleProduct
{
    public long SaleId { get; set; }
    
    [JsonIgnore]
    public Sale Sale { get; set; } = null!;

    public long ProductId { get; set; }
    public Product.Product Product { get; set; } = null!;
    
    public int TotalQuantitySoldPerProduct { get; set; }
    public decimal TotalPricePerProduct { get; set; }

    public string ReferenceNumber { get; set; } = null!;
}