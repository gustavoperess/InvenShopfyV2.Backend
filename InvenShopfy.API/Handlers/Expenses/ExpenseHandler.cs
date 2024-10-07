using InvenShopfy.API.Data;
using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Expenses;

public class ExpenseHandler (AppDbContext context) : IExpenseHandler
{
     public async Task<Response<Expense?>> CreateAsync(CreateExpenseRequest request)
    {
        try
        {
        
            var expense = new Expense
            {
                UserId = request.UserId,
                Date = request.Date,
                ExpenseType = request.ExpenseType,
                WarehouseId = request.WarehouseId,
                ExpenseCategoryId = request.ExpenseCategoryId,
                VoucherNumber = request.VoucherNumber,
                Amount = request.Amount,
                PurchaseNote = request.PurchaseNote
                
            };
            await context.Expenses.AddAsync(expense);
            await context.SaveChangesAsync();

            return new Response<Expense?>(expense, 201, "Expense  created successfully");

        }
        catch
        {
            return new Response<Expense?>(null, 500, "It was not possible to create a new Expense");
        }
    }

    public async Task<Response<Expense?>> UpdateAsync(UpdateExpenseRequest request)
    {
        try
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (expense is null)
            {
                return new Response<Expense?>(null, 404, "Expense not found");
            }
            
            expense.WarehouseId = request.WarehouseId;
            expense.ExpenseType = request.ExpenseType;
            expense.ExpenseCategoryId = request.ExpenseCategoryId;
            expense.VoucherNumber = request.VoucherNumber;
            expense.Amount = request.Amount;
            expense.PurchaseNote = request.PurchaceNote;
            
            context.Expenses.Update(expense);
            await context.SaveChangesAsync();
            return new Response<Expense?>(expense, message: "Expense updated successfully");

        }
        catch 
        {
            return new Response<Expense?>(null, 500, "It was not possible to update this Expense");
        }
    }

    public async Task<Response<Expense?>> DeleteAsync(DeleteExpenseRequest request)
    {
        try
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (expense is null)
            {
                return new Response<Expense?>(null, 404, "Expense  not found");
            }

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();
            return new Response<Expense?>(expense, message: "Expense  removed successfully");

        }
        catch 
        {
            return new Response<Expense?>(null, 500, "It was not possible to delete this Expense");
        }
    }
    
    public async Task<Response<Expense?>> GetByIdAsync(GetExpenseByIdRequest request)
    {
        try
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (expense is null)
            {
                return new Response<Expense?>(null, 404, "Expense  not found");
            }

            return new Response<Expense?>(expense);

        }
        catch 
        {
            return new Response<Expense?>(null, 500, "It was not possible to find this Expense");
        }
    }
    public async Task<PagedResponse<List<Expense>?>> GetByPeriodAsync(GetAllExpensesRequest request)
    {
        try
        {
            var query = context
                .Expenses
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.ExpenseType);
            
            var expense = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Expense>?>(
                expense,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Expense>?>(null, 500, "It was not possible to consult all Expense");
        }
    }
}