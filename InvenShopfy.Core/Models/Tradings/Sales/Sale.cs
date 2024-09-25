using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Models.Tradings.Sales
{
    public class Sale
    {
        public long Id { get; set; }
        private static readonly Random random = new Random();
        public DateTime Date { get; set; } = DateTime.UtcNow;
        
        public long CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        
        public long WarehouseId { get; set; }
        public Warehouse.Warehouse Warehouse { get; set; } = null!;

        public long BillerId { get; set; }
        public Biller Biller { get; set; } = null!;

        public List<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
        public double ShippingCost { get; set; } = 0.0;
        public EPaymentStatus PaymentStatus { get; set; }
        public ESaleStatus SaleStatus { get; set; }
        public string Document { get; set; } = string.Empty;
        public string SaleNote { get; set; } = string.Empty;
        public string StaffNote { get; set; } = string.Empty;

        public int TotalQuantitySold { get; set; }
        public string UserId { get; set; } = string.Empty;
        public double TotalAmount { get; set; } 

        public string ReferenceNumber { get; private set; }

        public int Discount { get; set; }
        
        public Sale()
        {
            ReferenceNumber = GenerateRandomNumber(); 
        }
        
        private static string GenerateRandomNumber()
        {
            char letter = (char)random.Next('A', 'Z' + 1); 
            int randNum = random.Next(1000000); 
            return letter + "-" + randNum.ToString("D6"); 
        }
        
        public SaleProduct CreateSaleProduct(long productId, double totalPricePerProduct, int totalQuantitySoldPerProduct)
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