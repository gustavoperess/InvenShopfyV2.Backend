using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Expenses;

public interface IExpenseHandler
{
    Task<Response<Models.Expenses.Expense?>> CreateExpenseAsync(CreateExpenseRequest request);
    Task<Response<Models.Expenses.Expense?>> UpdateExpenseAsync(UpdateExpenseRequest request);
    Task<Response<Models.Expenses.Expense?>> DeleteExpenseAsync(DeleteExpenseRequest request);
    Task<Response<Models.Expenses.Expense?>> GetExpenseByIdAsync(GetExpenseByIdRequest request);
    Task<PagedResponse<List<Models.Expenses.ExpenseDto>?>> GetExpenseByPeriodAsync(GetAllExpensesRequest request);
}