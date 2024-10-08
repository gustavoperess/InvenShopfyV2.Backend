using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.Warehouse;

namespace invenShopfy.Tests.HelperTest;

public class TestHelper
{
    public static readonly string UserId = "gustavo@gmail.com";
    public static Warehouse ReturnWarehouse(int warehouseId)
    {
        var warehouse01 = new Warehouse(1, "v76s", "Warehouse 01", "999-999-99", "warehouse01@gmail.com",
            "Judde house 1008", UserId);
        var warehouse02 = new Warehouse(2, "x34z", "Warehouse 02", "888-888-88", "warehouse02@gmail.com",
            "Main street 123", UserId);

        return warehouseId switch
        {
            1 => warehouse01,
            2 => warehouse02,
            _ => throw new ArgumentException("Invalid Warehouse")
        };
    }

    public static ExpenseCategory ReturnExpenseCategory(int expenseCategoryId)
    {
        var expenseCategory01 = new ExpenseCategory(1, "Office Supplies", "Stationery", UserId); 
        var expenseCategory02 = new ExpenseCategory(2, "Cleaning", "Cleaning sub category", UserId); 
        
        return expenseCategoryId switch
        {
            1 => expenseCategory01,
            2 => expenseCategory02,
            _ => throw new ArgumentException("Invalid expensive category")
        };
    }
    
    public static Customer ReturnCustomer(int customerId)
    {
        var customer01 = new Customer(1,"John Doe","johndoe@example.com","123-456-7890","New York","USA","123 Main St","BW1-string",100,"Local",UserId); 
        var customer02 = new Customer(2,"Walk In","Walk In@gmail.com","123-456-7890","New York","USA","123 Main St","BW1-string",100,"Local", UserId); 
        
        return customerId switch
        {
            1 => customer01,
            2 => customer02,
            _ => throw new ArgumentException("Invalid Customer")
        };
    }
    
}