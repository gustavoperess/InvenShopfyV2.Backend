using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.Handlers.Expenses;

public class ExpensePaymentHandler(AppDbContext context) : IExpensePaymentHandler
{
    public async Task<Response<ExpensePayment>?> CreateExpensePaymentAsync(CreateExpensePaymentRequest request)
    {
        try
        {
            var expensePayment = new ExpensePayment
            {
                UserId = request.UserId,
                ExpenseId = request.ExpenseId,
                Date = request.Date,
                PaymentType = request.ExpenseType
            };

            return new Response<ExpensePayment?>(null, 500, "It was not possible to create a new payment Expense");
        }
        catch (Exception e)
        {
            return new Response<ExpensePayment?>(null, 500, "It was not possible to create a new payment Expense");
        }
    }
}