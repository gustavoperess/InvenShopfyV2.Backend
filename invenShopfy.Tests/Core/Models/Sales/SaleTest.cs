namespace invenShopfy.Tests.Core.Models.Sales;

public class SaleTest
{
    
    private readonly DateOnly _saleDate = DateOnly.FromDateTime(DateTime.Now);
    private readonly string _expenseType = "Completed";
    private readonly long _voucherNumber = 1000;
    private readonly decimal _amount = 100;
    private readonly string _purchaseNote = "Completed";
    
    
}


// public long Id { get; set; }
// public DateOnly SaleDate { get; set; }  = DateOnly.FromDateTime(DateTime.Now);
// public long CustomerId { get; set; }
// public Customer Customer { get; set; } = null!;
// public long WarehouseId { get; set; }
// public Warehouse.Warehouse Warehouse { get; set; } = null!;
// public long BillerId { get; set; }
// public Biller Biller { get; set; } = null!;
// public decimal ShippingCost { get; set; } 
// public string PaymentStatus { get; set; } = string.Empty;
// public string SaleStatus { get; set; } = string.Empty; 
// public string Document { get; set; } = string.Empty;
// public string SaleNote { get; set; } = string.Empty;
// public string StaffNote { get; set; } = string.Empty;
// public int TotalQuantitySold { get; set; }
// public string UserId { get; set; } = string.Empty;
// public decimal TotalAmount { get; set; } 
// public string ReferenceNumber { get; private set; } = GenerateRandomNumber.RandomNumberGenerator();
// public int Discount { get; set; }
// public List<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();