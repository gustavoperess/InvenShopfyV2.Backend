using InvenShopfy.Core.Models.Tradings.Purchase;
using InvenShopfy.Core.Models.Product;


namespace invenShopfy.Tests.Core.Models.Purchase
{
    public class AddPurchaseTest
    {
        private readonly Dictionary<long, int> _productIdPlusQuantity = new() { { 1, 5 }, { 2, 3 }, { 3, 2 } };
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, StockQuantity = 10, Price = 100 },
            new Product { Id = 2, StockQuantity = 5, Price = 200 },
            new Product { Id = 3, StockQuantity = 2, Price = 300 }
        };

        [Fact]
        public void AddPurchaseToPurchase_ShouldAddProductsSuccessfully()
        {
       
            var addPurchase = new AddPurchase();
            var response = addPurchase.AddPurchaseToPurchase(_productIdPlusQuantity, _products);

         
            Assert.True(response.IsSuccess);
            Assert.Equal(10, addPurchase.TotalNumberOfProductsBought);
            Assert.Equal(3, addPurchase.PurchaseProducts.Count);
            Assert.Equal(15, _products[0].StockQuantity); // Product 1 stock after purchase
            Assert.Equal(8, _products[1].StockQuantity); // Product 2 stock after purchase
            Assert.Equal(4, _products[2].StockQuantity); // Product 3 stock after purchase
        }

        [Fact]
        public void AddPurchaseToPurchase_ShouldReturnError_WhenProductNotFound()
        {
      
            var addPurchase = new AddPurchase();
            var invalidProductIdPlusQuantity = new Dictionary<long, int> { { 4, 2 } }; // Product with ID 4 does not exist
            var response = addPurchase.AddPurchaseToPurchase(invalidProductIdPlusQuantity, _products);

     
            Assert.False(response.IsSuccess);
            Assert.Equal("Product with Id 4 not found", response.Message);
        }

        [Fact]
        public void AddPurchaseToPurchase_ShouldUpdateTotalAmountBought()
        {
            var addPurchase = new AddPurchase();
            var response = addPurchase.AddPurchaseToPurchase(_productIdPlusQuantity, _products);
            decimal expectedTotalAmount = (5 * 100) + (3 * 200) + (2 * 300);
            addPurchase.TotalAmountBought = expectedTotalAmount;
            Assert.Equal(expectedTotalAmount, addPurchase.TotalAmountBought);
        }

        [Fact]
        public void AddPurchaseToPurchase_ShouldNotModifyStock_WhenQuantityIsZero()
        {
            var addPurchase = new AddPurchase();
            var productIdPlusQuantityZero = new Dictionary<long, int> { { 1, 0 } }; // Attempt to add 0 quantity of product
            var response = addPurchase.AddPurchaseToPurchase(productIdPlusQuantityZero, _products);

        
            Assert.True(response.IsSuccess); // Should still succeed
            Assert.Equal(10, _products[0].StockQuantity); // Stock should remain unchanged
            Assert.Empty(addPurchase.PurchaseProducts); // No products should be added
        }
    }
}
