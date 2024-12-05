using InvenShopfy.API.Data;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses.ExpensePayment;
using InvenShopfy.Core.Requests.Expenses.ExpensePayment;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Expenses;

public class ExpensePaymentHandler(AppDbContext context) : IExpensePaymentHandler
{
    public async Task<Response<ExpensePayment?>> CreateExpensePaymentAsync(CreateExpensePaymentRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new Response<ExpensePayment?>(null, 409, $"{Configuration.NotAuthorized} 'create'");
            }
            
            var expense = context.Expenses.FirstOrDefault(x => x.Id == request.ExpenseId);
            if (expense != null)
            {
                if (expense.ExpenseStatus == "Paid")
                {
                    return new Response<ExpensePayment?>(null, 500, "This Expense has already been paid");
                  
                }
                expense.ExpenseStatus = "Paid";
                context.Expenses.Update(expense);
            }
            var expensePayment = new ExpensePayment
            {
                UserId = request.UserId,
                ExpenseId = request.ExpenseId,
                Date = request.Date,
                PaymentType = request.PaymentType,
                CardNumber = request.CardNumber,
                ExpenseNote = request.ExpenseNote,
            };
            
            await context.ExpensesPayments.AddAsync(expensePayment);
            await context.SaveChangesAsync();

            return new Response<ExpensePayment?>(expensePayment, 201, "Payment Expense created successfully");
        }
        catch
        {
            return new Response<ExpensePayment?>(null, 500, "It was not possible to create a new payment Expense");
        }
    }

    public async Task<Response<ExpensePaymentDto?>> GetExpensePaymentByIdAsync(GetExpensePaymentByIdRequest request)
    {
        try
        {
            var expensePayment = await context.ExpensesPayments
                .Include(x => x.Expense)
                .Include(x => x.Expense.ExpenseCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (expensePayment is null)
            {
                return new Response<ExpensePaymentDto?>(null, 404, "It was not possible to find this ExpensePayment");
            }
            
            var result = new ExpensePaymentDto
            {
                Id = expensePayment.Id,
                Date = expensePayment.Date,
                VoucherNumber = expensePayment.Expense.VoucherNumber,
                PaymentType = expensePayment.PaymentType,
                ExpenseCost = expensePayment.Expense.ExpenseCost,
                ExpenseCategory = expensePayment.Expense.ExpenseCategory.MainCategory,
                CardNumber = $"**** **** **** {expensePayment.CardNumber.Substring(expensePayment.CardNumber.Length - 4)}"
            };
            return new Response<ExpensePaymentDto?>(result, 201, "Payment Expense retuned successfully");
        }
        catch
        {
            return new Response<ExpensePaymentDto?>(null, 500, "It was not possible to find this Expense");
        }
    }
}