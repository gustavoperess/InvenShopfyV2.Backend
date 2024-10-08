using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.Warehouse;

namespace invenShopfy.Tests.NewClassesForTest;

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
            _ => throw new ArgumentException("Invalid Warehouse 1")
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
            _ => throw new ArgumentException("Invalid expensive category 1")
        };
    }
    
}