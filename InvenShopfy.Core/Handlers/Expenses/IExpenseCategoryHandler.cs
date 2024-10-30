using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Expenses;

public interface IExpenseCategoryHandler
{
    Task<Response<Models.Expenses.ExpenseCategory?>> CreateExpenseCategoryAsync(CreateExpenseCategoryRequest request);
    Task<Response<Models.Expenses.ExpenseCategory?>> UpdateExpenseCategoryAsync(UpdateExpenseCategoryRequest request);
    Task<Response<Models.Expenses.ExpenseCategory?>> DeleteExpenseCategoryAsync(DeleteExpenseCategoryRequest request);
    Task<Response<Models.Expenses.ExpenseCategory?>> GetExpenseCategoryByIdAsync(GetExpenseCategoryByIdRequest request);
    Task<PagedResponse<List<Models.Expenses.ExpenseCategory>?>> GetExpenseCategoryByPeriodAsync(GetAllExpensesCategoriesRequest request);
}