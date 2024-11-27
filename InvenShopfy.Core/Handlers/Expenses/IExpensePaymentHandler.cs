using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.Expenses.ExpensePayment;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpensePayment;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Expenses;

public interface IExpensePaymentHandler
{
    Task<Response<ExpensePayment?>> CreateExpensePaymentAsync(CreateExpensePaymentRequest request);
    
    Task<List<Response<ExpensePayment?>>> GetExpensePaymentAsync(CreateExpensePaymentRequest request);


}