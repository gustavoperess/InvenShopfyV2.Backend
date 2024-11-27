using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Expenses.ExpensePayment;
using InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Requests.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.Handlers.Tradings.Sales;

public class SalesPaymentHandler(AppDbContext context) : ISalesPaymentHandler
{
    public async Task<Response<SalesPayment?>> CreateSalesPaymentAsync(CreateSalesPaymentRequest request)
    {
        try
        {
            var expense = context.Sales.FirstOrDefault(x => x.Id == request.SalesId);
            if (expense != null)
            {
                if (expense.SaleStatus == "Paid")
                {
                    return new Response<SalesPayment?>(null, 500, "This Expense has already been paid");
                  
                }
                expense.SaleStatus = "Paid";
                context.Sales.Update(expense);
            }
            var expensePayment = new SalesPayment
            {
                UserId = request.UserId,
                SalesId = request.SalesId,
                Date = request.Date,
                PaymentType = request.PaymentType,
                CardNumber = request.CardNumber,
                SalesNote= request.SalesNote,
            };
            
            await context.SalesPayments.AddAsync(expensePayment);
            await context.SaveChangesAsync();

            return new Response<SalesPayment?>(expensePayment, 201, "Payment Expense created successfully");
        }
        catch
        {
            return new Response<SalesPayment?>(null, 500, "It was not possible to create a new payment Expense");
        }
    }

    // public async Task<Response<ExpensePaymentDto?>> GetExpensePaymentByIdAsync(GetExpensePaymentByIdRequest request)
    // {
    //     try
    //     {
    //         var expensePayment = await context.ExpensesPayments
    //             .Include(x => x.Expense)
    //             .Include(x => x.Expense.ExpenseCategory)
    //             .AsNoTracking()
    //             .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
    //
    //         if (expensePayment is null)
    //         {
    //             return new Response<ExpensePaymentDto?>(null, 404, "It was not possible to find this ExpensePayment");
    //         }
    //         
    //         var result = new ExpensePaymentDto
    //         {
    //             Id = expensePayment.Id,
    //             Date = expensePayment.Date,
    //             VoucherNumber = expensePayment.Expense.VoucherNumber,
    //             PaymentType = expensePayment.PaymentType,
    //             ExpenseCost = expensePayment.Expense.ExpenseCost,
    //             ExpenseCategory = expensePayment.Expense.ExpenseCategory.MainCategory,
    //             CardNumber = $"**** **** **** {expensePayment.CardNumber.Substring(expensePayment.CardNumber.Length - 4)}"
    //         };
    //         return new Response<ExpensePaymentDto?>(result, 201, "Payment Expense retuned successfully");
    //     }
    //     catch
    //     {
    //         return new Response<ExpensePaymentDto?>(null, 500, "It was not possible to find this Expense");
    //     }
    // }
}