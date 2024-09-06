using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Expenses;

public interface IExpenseHandler
{
    Task<Response<Models.Expenses.Expense?>> CreateAsync(CreateExpenseRequest request);
    Task<Response<Models.Expenses.Expense?>> UpdateAsync(UpdateExpenseRequest request);
    Task<Response<Models.Expenses.Expense?>> DeleteAsync(DeleteExpenseRequest request);
    Task<Response<Models.Expenses.Expense?>> GetByIdAsync(GetExpenseByIdRequest request);
    Task<PagedResponse<List<Models.Expenses.Expense>?>> GetByPeriodAsync(GetAllExpensesRequest request);
}