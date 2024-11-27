using InvenShopfy.Core.Models.Expenses.ExpenseCategory;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Expenses;

public interface IExpenseCategoryHandler
{
    Task<Response<ExpenseCategory?>> CreateExpenseCategoryAsync(CreateExpenseCategoryRequest request);
    Task<Response<ExpenseCategory?>> UpdateExpenseCategoryAsync(UpdateExpenseCategoryRequest request);
    Task<Response<ExpenseCategory?>> DeleteExpenseCategoryAsync(DeleteExpenseCategoryRequest request);
    Task<Response<ExpenseCategory?>> GetExpenseCategoryByIdAsync(GetExpenseCategoryByIdRequest request);
    Task<PagedResponse<List<ExpenseCategory>?>> GetExpenseCategoryByPeriodAsync(GetAllExpensesCategoriesRequest request);
}