using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Models.Tradings.Sales;
using InvenShopfy.Core.Models.Warehouse;

namespace invenShopfy.Tests.HelperTest;

public class TestHelper
{
    // public static readonly string UserId = "gustavo@gmail.com";
    //
    // public static Unit ReturnProductUnit(int unitId)
    // {
    //     var unit01 = new Unit(1, "Kilogram", "kg", UserId);
    //     var unit02 = new Unit(2, "Meter", "mt", UserId);
    //
    //     return unitId switch
    //     {
    //         1 => unit01,
    //         2 => unit02,
    //         _ => throw new ArgumentException("Invalid Unit")
    //     };
    // }
    //
    // public static Warehouse ReturnWarehouse(int warehouseId)
    // {
    //     var warehouse01 = new Warehouse(1, "v76s", "Warehouse 01", "999-999-99", "warehouse01@gmail.com",
    //         "Judde house 1008", UserId);
    //     var warehouse02 = new Warehouse(2, "x34z", "Warehouse 02", "888-888-88", "warehouse02@gmail.com",
    //         "Main street 123", UserId);
    //
    //     return warehouseId switch
    //     {
    //         1 => warehouse01,
    //         2 => warehouse02,
    //         _ => throw new ArgumentException("Invalid Warehouse")
    //     };
    // }
    //
    // public static ExpenseCategory ReturnExpenseCategory(int expenseCategoryId)
    // {
    //     var expenseCategory01 = new ExpenseCategory(1, "Office Supplies", "Stationery", UserId);
    //     var expenseCategory02 = new ExpenseCategory(2, "Cleaning", "Cleaning sub category", UserId);
    //
    //     return expenseCategoryId switch
    //     {
    //         1 => expenseCategory01,
    //         2 => expenseCategory02,
    //         _ => throw new ArgumentException("Invalid expense category")
    //     };
    // }
    //
    // public static Customer ReturnCustomer(int customerId)
    // {
    //     var customer01 = new Customer(1, "John Doe", "johndoe@example.com", "123-456-7890", "New York", "USA",
    //         "123 Main St", "BW1-string", 100, "Local", UserId);
    //     var customer02 = new Customer(2, "Walk In", "Walk In@gmail.com", "123-456-7890", "New York", "USA",
    //         "123 Main St", "BW1-string", 100, "Local", UserId);
    //
    //     return customerId switch
    //     {
    //         1 => customer01,
    //         2 => customer02,
    //         _ => throw new ArgumentException("Invalid Customer")
    //     };
    // }
    //
    // public static Biller ReturnBiller(int billerId)
    // {
    //     var warehouse = ReturnWarehouse(1);
    //     var biller01 = new Biller(1, "Biller A", new DateOnly(2024, 09, 11), "biller_a@example.com", "1234567890",
    //         "ID1234", "123 Billing St.", "USA", "10004", 101, warehouse.Id, UserId);
    //     var biller02 = new Biller(2, "Biller B", new DateOnly(2024, 09, 12), "biller_b@example.com", "0987654321",
    //         "ID5678", "456 Billing Ave.", "USA", "90004", 102, warehouse.Id, UserId);
    //
    //     return billerId switch
    //     {
    //         1 => biller01,
    //         2 => biller02,
    //         _ => throw new ArgumentException("Invalid Biller")
    //     };
    // }
    //
    // public static Brand ReturnProductBrand(int brandId)
    // {
    //     var brand01 = new Brand(1,"Apple","https://res.cloudinary.com/dououppib/image/upload/v1727443096/invenShopfy/Brands/vrflz2shr2eu9itg0q2h.jpg", UserId);
    //     var brand02 = new Brand(3, "Microsoft", "https://res.cloudinary.com/dououppib/image/upload/v1727443149/invenShopfy/Brands/vfza8ddm0smnjgrw7ec0.jpg", UserId);
    //
    //     return brandId switch
    //     {
    //         1 => brand01,
    //         2 => brand02,
    //         _ => throw new ArgumentException("Invalid brand")
    //     };
    // }
    //
    //
    // public static Category ReturnProductCategory(int categoryId)
    // {
    //     var category01 = new Category(
    //         1,
    //         "Electronic",
    //         new List<string> { "Computer", "Cellphone" },
    //         UserId
    //     );
    //     var category02 = new Category(
    //         2,
    //         "Clothing",
    //         new List<string> { "Shoes", "Nike" },
    //         UserId
    //     );
    //
    //     return categoryId switch
    //     {
    //         1 => category01,
    //         2 => category02,
    //         _ => throw new ArgumentException("Invalid Product Category")
    //     };
    // }
    //
    // // Updated ReturnSale method using the hardcoded values
    // public static Sale ReturnSale(int saleId)
    // {
    //     var customer = ReturnCustomer(1);
    //     var warehouse = ReturnWarehouse(1);
    //     var biller = ReturnBiller(1);
    //
    //     var sale01 = new Sale(
    //         1, // id
    //         DateOnly.FromDateTime(DateTime.Now), // saleDate directly added
    //         customer.Id, // customer id
    //         warehouse.Id, // warehouse id
    //         biller.Id, // biller id
    //         100m, // shipping cost directly added
    //         "Payment status Completed", // payment status directly added
    //         "Sale status Completed", // sale status directly added
    //         "Not attached", // document directly added
    //         "Sale Note Completed", // sales note directly added
    //         "Staff Note Completed", // staff note directly added
    //         5, // total quantity sold directly added
    //         1000m, // total amount directly added
    //         10, // discount directly added
    //         UserId // user id
    //     );
    //
    //     return saleId switch
    //     {
    //         1 => sale01,
    //         _ => throw new ArgumentException("Invalid Sale")
    //     };
    // }
    //

    // public static Product ReturnProduct(int productId)
    // {
    //
    //     var unit01 = ReturnProductUnit(1);
    //     var brand01 = ReturnProductBrand(1);
    //     var category01 = ReturnProductCategory(1);
    //     var product01 = new Product(
    //         1, // Id: long
    //         3121.00m, // Price: decimal
    //         123123, // ProductCode: int
    //         100, // StockQuantity: int
    //         unit01.Id, // UnitId: long
    //         brand01.Id, // BrandId: long
    //         category01.Id, // CategoryId: long
    //         "https://res.cloudinary.com/dououppib/image/upload/v1728311668/invenShopfy/Products/prxctztztqpglbo8p4ye.png", // ProductImage: string
    //         false, // Featured: bool
    //         false, // DifferPriceWarehouse: bool
    //         false, // Expired: bool
    //         false, // Sale: bool
    //         UserId // UserId: string
    //     );
    //     var unit02 = ReturnProductUnit(2);
    //     var brand02 = ReturnProductBrand(2);
    //     var category02 = ReturnProductCategory(2);
    //     var product02 = new Product(
    //         2, // Id: long
    //         100.00m, // Price: decimal
    //         123, // ProductCode: int
    //         50, // StockQuantity: int
    //         unit02.Id, // UnitId: long
    //         brand02.Id, // BrandId: long
    //         category02.Id, // CategoryId: long
    //         "https://res.cloudinary.com/dououppib/image/upload/v1728311668/invenShopfy/Products/prxctztztqpglbo8p4ye.png", // ProductImage: string
    //         true, // Featured: bool
    //         true, // DifferPriceWarehouse: bool
    //         true, // Expired: bool
    //         true, // Sale: bool
    //         UserId // UserId: string
    //     );
    //
    //     return productId switch
    //     {
    //         1 => product01,
    //         2 => product02,
    //         _ => throw new ArgumentException("Invalid Product")
    //     };
    // }
}
