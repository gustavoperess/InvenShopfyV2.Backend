using InvenShopfy.Core.Common.RandomNumber;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Responses;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("InvenShopfy.Tests")]
namespace InvenShopfy.Core.Models.Tradings.Sales
{
 
    public sealed class Sale
    {
        public long Id { get; init; }
        public DateOnly SaleDate { get; init; }  = DateOnly.FromDateTime(DateTime.Now);
        public long CustomerId { get; set; }
        public Customer Customer { get; init; } = null!;
        public long WarehouseId { get; set; }
        public Warehouse.Warehouse Warehouse { get; init; } = null!;
        public long BillerId { get; set; }
        public Biller Biller { get; init; } = null!;
        public decimal ShippingCost { get; set; } 
        public string PaymentStatus { get; init; } = string.Empty;
        public string SaleStatus { get; init; } = string.Empty; 
        public string SaleNote { get; set; } = string.Empty;
        public string StaffNote { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }

        public decimal TotalAmount { get; init; } 
        public string ReferenceNumber { get; init; } = GenerateRandomNumber.RandomNumberGenerator();
        public int Discount { get; init; }
        public List<SaleProduct> SaleProducts { get; init; } = new List<SaleProduct>();
        public string UserId { get; init; } = string.Empty;
        
        
        // Handle Sale logic 
        public Response<Sale?> AddProductsToSale(Dictionary<long, int> productIdPlusQuantity, List<Product.Product> availableProducts)
        {
            var listOfProducts = CheckIfProductIsAvalible(productIdPlusQuantity, availableProducts);
            foreach (var item in listOfProducts)
            {
                var quantityToSell = int.Parse(item.Value);
                if (quantityToSell == 0)
                {
                    return new Response<Sale?>(null, 400, $"Some products with id's {item.Key} are unavailable or insufficient in stock: {item.Value}");
                }
                
                var product = availableProducts.FirstOrDefault(po => po.Id == item.Key);
                if (product != null)
                {
                    var pricePerProduct = product.Price * quantityToSell;
                    var saleProduct = CreateSaleProduct(product.Id, pricePerProduct, quantityToSell);
                    SaleProducts.Add(saleProduct);
                    product.StockQuantity -= quantityToSell;
                }
        
            }
            TotalQuantitySold = SaleProducts.Sum(x => x.TotalQuantitySoldPerProduct);
            return new Response<Sale?>(this, 200, "Products added to sale successfully");
        }

        internal Dictionary<long, string> CheckIfProductIsAvalible(Dictionary<long, int> productIdPlusQuantity,  List<Product.Product> availableProducts)
        {
            var listOfAvaliableProducts = new Dictionary<long, string>();
            foreach (var item in productIdPlusQuantity)
            {
                var product = availableProducts.FirstOrDefault(po => po.Id == item.Key);
                if (product != null)
                {
                    if (product.StockQuantity >= item.Value)
                    {
                        listOfAvaliableProducts[item.Key] = item.Value.ToString();
                    }
                    else if (item.Value > product.StockQuantity)
                    { // if the request is > than the requested quantity, then we add the request quantity
                        listOfAvaliableProducts[item.Key] = product.StockQuantity.ToString(); 
                        
                    } 
                }
            }
            
            return listOfAvaliableProducts;
        }
        
        
        // creates the sale product
        internal SaleProduct CreateSaleProduct(long productId, decimal totalPricePerProduct, int totalQuantitySoldPerProduct)
        {
            var saleProduct = new SaleProduct
            {
                ProductId = productId,
                Sale = this,
                TotalPricePerProduct = totalPricePerProduct,
                TotalQuantitySoldPerProduct = totalQuantitySoldPerProduct,
                ReferenceNumber = ReferenceNumber 
            };
            return saleProduct;
        }
        
    }
}