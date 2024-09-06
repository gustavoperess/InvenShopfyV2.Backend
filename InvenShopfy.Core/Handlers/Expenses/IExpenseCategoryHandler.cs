using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Expenses;

public interface IExpenseCategoryHandler
{
    Task<Response<Models.Expenses.ExpenseCategory?>> CreateAsync(CreateExpenseCategoryRequest request);
    Task<Response<Models.Expenses.ExpenseCategory?>> UpdateAsync(UpdateExpenseCategoryRequest request);
    Task<Response<Models.Expenses.ExpenseCategory?>> DeleteAsync(DeleteExpenseCategoryRequest request);
    Task<Response<Models.Expenses.ExpenseCategory?>> GetByIdAsync(GetExpenseCategoryByIdRequest request);
    Task<PagedResponse<List<Models.Expenses.ExpenseCategory>?>> GetByPeriodAsync(GetAllExpensesCategoriesRequest request);
}