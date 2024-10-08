using InvenShopfy.Core.Common.RandomNumber;

namespace invenShopfy.Tests.Core.Models.Sales;

public class SaleTest
{
    
    private readonly DateOnly _saleDate = DateOnly.FromDateTime(DateTime.Now);
    private readonly string _expenseType = "Completed";
    private readonly long _voucherNumber = 1000;
    private readonly decimal _amount = 1000;
    private readonly string _purchaseNote = "Puchase Completed";
    private readonly decimal _shippingCost = 100;
    private readonly string _paymentStatus = "Payment Completed";
    private readonly string _saleStatus = " Sale status Completed";
    private readonly string _document = "Not attached";
    private readonly string _saleNote = "Sale Note Completed";
    private readonly string _staffNote = "Staff Note Completed";
    private readonly int _totalQuantitySold = 5;
    private readonly decimal _totalAmount = 100;
    private readonly string ReferenceNumber = GenerateRandomNumber.RandomNumberGenerator();
    private readonly int _discount = 10;

}


// public string ReferenceNumber { get; private set; } = GenerateRandomNumber.RandomNumberGenerator();
// public int Discount { get; set; }
// public List<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();