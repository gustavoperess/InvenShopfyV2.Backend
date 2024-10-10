using System.Text.Json.Serialization;
namespace InvenShopfy.Core.Models.Tradings.Sales;

public class SaleProduct
{
    public long SaleId { get; init; }
    [JsonIgnore]
    public Sale Sale { get; init; } = null!;
    public long ProductId { get; init; }
    public Product.Product Product { get; init; } = null!;
    public int TotalQuantitySoldPerProduct { get; init; }
    public decimal TotalPricePerProduct { get; init; }
    public string ReferenceNumber { get; init; } = null!;
}