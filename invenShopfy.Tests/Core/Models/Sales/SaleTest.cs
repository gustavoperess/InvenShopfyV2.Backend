using InvenShopfy.Core.Common.RandomNumber;
using InvenShopfy.Core.Models.Product;
using invenShopfy.Tests.HelperTest;
using InvenShopfy.Core.Models.Tradings.Sales;

namespace invenShopfy.Tests.Core.Models.Sales;

public class SaleTest
{
    private readonly Dictionary<long, int> _productIdPlusQuantity = new() { { 1, 5 }, { 2, 10 }, { 3, 2 } };
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, StockQuantity = 10, Price = 100 },
        new Product { Id = 2, StockQuantity = 8, Price = 200 },
        new Product { Id = 3, StockQuantity = 2, Price = 300 }
    };
    private readonly Sale _sale = new Sale();
    private readonly long _productId = 1;
    private readonly decimal _totalPrice = 100m;
    private readonly int _quantitySold = 5;
    
    
    [Fact]
    public void AddProductsToSale_Calculates_TotalQuantitySold_Correctly()
    {

        var sale = new Sale();
        var response = sale.AddProductsToSale(_productIdPlusQuantity, _products);
        
        Assert.True(response.IsSuccess);
        Assert.Equal(15, sale.TotalQuantitySold); // 5 + 10 + 2 = 17
    }

    
    [Fact]
    public void check_if_product_price_is_calculated_correct()
    {
        var sale = new Sale();
        var response = sale.CheckIfProductIsAvalible(_productIdPlusQuantity, _products);
        var results = new List<decimal>();
        foreach (var item in response)
        {
            var product = _products.FirstOrDefault(p => p.Id == item.Key);
            if (product != null)
            {
                decimal r = product.Price * int.Parse(item.Value);
                results.Add(r);
            }
        }
        Assert.Equal(500, results[0]);
        Assert.Equal(1600, results[1]);
        Assert.Equal(600, results[2]);
    }

    [Fact]
    public void check_if_product_os_added_to_salesproduct()
    {
        var saleProduct = _sale.CreateSaleProduct(_productId, _totalPrice, _quantitySold);
        Assert.NotNull(saleProduct);
        Assert.Equal(_productId, saleProduct.ProductId); 
        Assert.Equal(_totalPrice, saleProduct.TotalPricePerProduct); 
        Assert.Equal(_quantitySold, saleProduct.TotalQuantitySoldPerProduct); 
        Assert.Equal(_sale.ReferenceNumber, saleProduct.ReferenceNumber); 
        Assert.Equal(_sale, saleProduct.Sale);
    }
    
    [Fact]
    public void Check_if_product_is_available_returns_correct_availability()
    {
        var sale = new Sale();
        var avalibleProducts = sale.CheckIfProductIsAvalible(_productIdPlusQuantity, _products);
        Assert.Equal("5", avalibleProducts[1]); 
        Assert.Equal("8", avalibleProducts[2]); 
        Assert.Equal("2", avalibleProducts[3]); 

    }
    
    [Fact]
    public void Check_if_product_stock_is_deducted()
    {
        var results = new List<decimal>();
        foreach (var item in _productIdPlusQuantity)
        {
            var product = _products.FirstOrDefault(p => p.Id == item.Key);
            var quantityToSell = item.Value;
            if (product != null)
            {
                product.StockQuantity -= quantityToSell;
                results.Add(product.StockQuantity);
            }
        }
        Assert.Equal(5, results[0]);
        Assert.Equal(-2, results[1]);
        Assert.Equal(0, results[2]);
    }

    [Fact]
    public void Check_if_can_add_to_sales()
    {
        var sale = new Sale();
        var response = sale.AddProductsToSale(_productIdPlusQuantity, _products);
        Assert.True(response.IsSuccess);
        Assert.Equal(3, sale.SaleProducts.Count);
    }
    
    
    [Fact]
    public void AddProductsToSale_Updates_SaleProducts_List()
    {
        var sale = new Sale();
        var response = sale.AddProductsToSale(_productIdPlusQuantity, _products);
        
        Assert.True(response.IsSuccess);
        Assert.Equal(3, sale.SaleProducts.Count);
        Assert.All(sale.SaleProducts, sp => Assert.NotNull(sp));
    }
    
   
    [Fact]
    public void AddProductsToSale_Deducts_Stock_Quantity_Correctly()
    {
        var sale = new Sale();
        var response = sale.AddProductsToSale(_productIdPlusQuantity, _products);
    
        Assert.True(response.IsSuccess);
        Assert.Equal(5, _products.First(p => p.Id == 1).StockQuantity); // 10 - 5 = 5
        Assert.Equal(0, _products.First(p => p.Id == 2).StockQuantity); // 8 - 8 = 0
        Assert.Equal(0, _products.First(p => p.Id == 3).StockQuantity); // 2 - 2 = 0
    }
    
    [Fact]
    public void CreateSaleProduct_Returns_SaleProduct_With_Correct_Values()
    {
        var saleProduct = _sale.CreateSaleProduct(_productId, _totalPrice, _quantitySold);
 
        Assert.NotNull(saleProduct);
        Assert.Equal(_productId, saleProduct.ProductId);
        Assert.Equal(_totalPrice, saleProduct.TotalPricePerProduct);
        Assert.Equal(_quantitySold, saleProduct.TotalQuantitySoldPerProduct);
        Assert.Equal(_sale.ReferenceNumber, saleProduct.ReferenceNumber);
        Assert.Equal(_sale, saleProduct.Sale);
    }

}

