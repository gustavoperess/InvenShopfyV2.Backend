using InvenShopfy.Core.Common.RandomNumber;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Models.Tradings.Sales
{
    public class Sale
    {
        public Sale() {}
        public Sale(long id, DateOnly? saleDate, long customerId, long warehouseId, long billerId, 
            decimal shippingCost, string paymentStatus, string saleStatus, string document, string saleNote,
            string staffNote, int totalQuantitySold, decimal totalAmount, int discount, string userId)
        {
            Id = id;
            SaleDate = saleDate ?? DateOnly.FromDateTime(DateTime.Now);
            CustomerId = customerId;
            WarehouseId = warehouseId;
            BillerId = billerId;
            ShippingCost = shippingCost;
            PaymentStatus = paymentStatus;
            SaleStatus = saleStatus;
            Document = document;
            SaleNote = saleNote;
            StaffNote = staffNote;
            TotalQuantitySold = totalQuantitySold;
            TotalAmount = totalAmount;
            ReferenceNumber = GenerateRandomNumber.RandomNumberGenerator();
            Discount = discount;
            UserId = userId;
        }

        public long Id { get; set; }
        public DateOnly SaleDate { get; set; }  = DateOnly.FromDateTime(DateTime.Now);
        public long CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public long WarehouseId { get; set; }
        public Warehouse.Warehouse Warehouse { get; set; } = null!;
        public long BillerId { get; set; }
        public Biller Biller { get; set; } = null!;
        public decimal ShippingCost { get; set; } 
        public string PaymentStatus { get; set; } = string.Empty;
        public string SaleStatus { get; set; } = string.Empty; 
        public string Document { get; set; } = string.Empty;
        public string SaleNote { get; set; } = string.Empty;
        public string StaffNote { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }

        public decimal TotalAmount { get; set; } 
        public string ReferenceNumber { get; private set; } = GenerateRandomNumber.RandomNumberGenerator();
        public int Discount { get; set; }
        public List<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
        
        public string UserId { get; set; } = string.Empty;
        
        // Handle Sale logic 
        public Response<Sale?> AddProductsToSale(Dictionary<long, int> productIdPlusQuantity, List<SaleProduct> availableProducts) 
        {
            foreach (var item in productIdPlusQuantity)
            {
                var product = availableProducts.FirstOrDefault(po => po.Product.Id == item.Key);
                if (product == null)
                {
                    return new Response<Sale?>(null, 400, $"Product with Id {item.Key} not found");
                }
                if (product.Product.StockQuantity < item.Value)
                {
                    return new Response<Sale?>(null, 400, $"Insufficient quantity for product Id {item.Key}");
                }
                var pricePerProduct = product.Product.Price * item.Value;
                var saleProduct = CreateSaleProduct(product.Product.Id, pricePerProduct, item.Value);
                SaleProducts.Add(saleProduct);
                product.Product.StockQuantity -= item.Value;
            }
            
            TotalQuantitySold = SaleProducts.Sum(x => x.TotalQuantitySoldPerProduct);
            return new Response<Sale?>(this, 200, "Products added to sale successfully");
        }
        
        
        // creates the sale product
        private SaleProduct CreateSaleProduct(long productId, decimal totalPricePerProduct, int totalQuantitySoldPerProduct)
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