using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.Expenses.ExpenseDto;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Expenses;

public interface IExpenseHandler
{
    Task<Response<Expense?>> CreateExpenseAsync(CreateExpenseRequest request);
    Task<Response<Expense?>> UpdateExpenseAsync(UpdateExpenseRequest request);
    Task<Response<Expense?>> DeleteExpenseAsync(DeleteExpenseRequest request);
    Task<Response<ExpenseUpdate?>> GetExpenseByIdAsync(GetExpenseByIdRequest request);
    Task<PagedResponse<List<ExpenseDto>?>> GetExpenseByPeriodAsync(GetAllExpensesRequest request);
    Task<Response<List<ExpenseDashboard>?>> GetExpenseStatusDashboardAsync(GetAllExpensesRequest request);
    Task<PagedResponse<List<ExpenseDto>?>> GetExpenseByPartialNumberAsync(GetExpenseByNumberRequest request);

    Task<Response<decimal?>> GetExpenseTotalAmountAsync();

}