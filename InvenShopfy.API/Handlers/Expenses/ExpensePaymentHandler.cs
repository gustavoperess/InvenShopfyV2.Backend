using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.Handlers.Expenses;

public class ExpensePaymentHandler(AppDbContext context) : IExpensePaymentHandler
{
    public async Task<Response<ExpensePayment?>> CreateExpensePaymentAsync(CreateExpensePaymentRequest request)
    {
        try
        {
            var expensePayment = new ExpensePayment
            {
                UserId = request.UserId,
                ExpenseId = request.ExpenseId,
                Date = request.Date,
                PaymentType = request.PaymentType,
                CardNumber = request.CardNumber,
                ExpenseNote = request.ExpenseNote,
            };

            var expense = context.Expenses.FirstOrDefault(x => x.Id == request.ExpenseId);
            if (expense != null)
            {
                if (expense.ExpenseStatus != "Paid")
                {
                    expense.ExpenseStatus = "Paid";
                    context.Expenses.Update(expense);
                }
            }

            await context.ExpensesPayments.AddAsync(expensePayment);
            await context.SaveChangesAsync();

            return new Response<ExpensePayment?>(expensePayment, 201, "Payment Expense created successfully");
        }
        catch
        {
            return new Response<ExpensePayment?>(null, 500, "It was not possible to create a new payment Expense");
        }
    }
}