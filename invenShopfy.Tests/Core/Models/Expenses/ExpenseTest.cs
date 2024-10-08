using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.Tradings.Sales;
using invenShopfy.Tests.HelperTest;

namespace invenShopfy.Tests.Core.Models.Expenses;

public class ExpenseTest
{
    private readonly DateOnly _date = DateOnly.FromDateTime(DateTime.Now);
    private readonly string _expenseType = "Completed";
    private readonly long _voucherNumber = 1000;
    private readonly decimal _amount = 100;
    private readonly string _purchaseNote = "Completed";
    
    [Fact]
    public void Creates_a_new_expense_model()
    {
        var warehouse = TestHelper.ReturnWarehouse(1);
        var expenseCategory = TestHelper.ReturnExpenseCategory(1);
        var userId = TestHelper.UserId;
        var expense = new Expense(1, _date, warehouse, _expenseType, expenseCategory, _voucherNumber, _amount, _purchaseNote, userId);
        Assert.Equal(1, expense.Id);
        Assert.Equal(_date, expense.Date);
        Assert.Equal(warehouse, expense.Warehouse);
        Assert.Equal(_expenseType, expense.ExpenseType);
        Assert.Equal(expenseCategory, expense.ExpenseCategory);
        Assert.Equal(_voucherNumber, expense.VoucherNumber);
        Assert.Equal(_amount, expense.Amount);
        Assert.Equal(_purchaseNote, expense.PurchaseNote);
        Assert.Equal(userId, expense.UserId);
    }
    
    [Fact]
    public void Can_set_expense_properties()
    {
        // Arrange
        var expense = new Expense();
        var newWarehouse = TestHelper.ReturnWarehouse(2);
        var newExpenseCategory = TestHelper.ReturnExpenseCategory(2);

        // Act
        expense.Id = 2;
        expense.Date = _date;
        expense.Warehouse = newWarehouse;
        expense.ExpenseType = "Updated";
        expense.ExpenseCategory = newExpenseCategory;
        expense.VoucherNumber = 2000;
        expense.Amount = 500;
        expense.PurchaseNote = "Updated note";
        expense.UserId = "NewUserId";

        // Assert
        Assert.Equal(2, expense.Id);
        Assert.Equal(_date, expense.Date);
        Assert.Equal(newWarehouse, expense.Warehouse);
        Assert.Equal("Updated", expense.ExpenseType);
        Assert.Equal(newExpenseCategory, expense.ExpenseCategory);
        Assert.Equal(2000, expense.VoucherNumber);
        Assert.Equal(500, expense.Amount);
        Assert.Equal("Updated note", expense.PurchaseNote);
        Assert.Equal("NewUserId", expense.UserId);
    }
}

