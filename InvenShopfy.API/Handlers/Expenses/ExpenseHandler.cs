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
     public async Task<Response<Expense?>> CreateExpenseAsync(CreateExpenseRequest request)
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
                ExpenseCost = request.ExpenseCost,
                ExpenseNote = request.ExpenseNote,
                ExpenseDescription = request.ExpenseDescription,
                ShippingCost = request.ShippingCost
                
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

    public async Task<Response<Expense?>> UpdateExpenseAsync(UpdateExpenseRequest request)
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
            expense.ExpenseCost = request.ExpenseCost;
            expense.ExpenseNote = request.ExpenseNote;
            expense.ExpenseDescription = request.ExpenseDescription;
            expense.ShippingCost = request.ShippingCost;
            
            context.Expenses.Update(expense);
            await context.SaveChangesAsync();
            return new Response<Expense?>(expense, message: "Expense updated successfully");

        }
        catch 
        {
            return new Response<Expense?>(null, 500, "It was not possible to update this Expense");
        }
    }

    public async Task<Response<Expense?>> DeleteExpenseAsync(DeleteExpenseRequest request)
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
    
    public async Task<Response<Expense?>> GetExpenseByIdAsync(GetExpenseByIdRequest request)
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
    public async Task<PagedResponse<List<ExpenseDto>?>> GetExpenseByPeriodAsync(GetAllExpensesRequest request)
    {
        try
        {
            var query = context
                .Expenses
                .AsNoTracking()
                .Include(x => x.Warehouse)
                .Include(x => x.ExpenseCategory)
                .Where(x => x.UserId == request.UserId)
                .Select(g => new
                {
                    g.Id,
                    g.ExpenseDescription,
                    g.Date,
                    g.Warehouse.WarehouseName,
                    g.ExpenseType,
                    ExpenseCategory = g.ExpenseCategory.Category,
                    g.VoucherNumber,
                    g.ExpenseCost,
                    g.ExpenseNote,
                    g.ShippingCost
                })
                .OrderBy(x => x.ExpenseType);
            
            var expense = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            var result = expense.Select(s => new ExpenseDto
            {
                Id = s.Id,
                ExpenseDescription = s.ExpenseDescription,
                Date = s.Date,
                WarehouseName =  s.WarehouseName,
                ExpenseType = s.ExpenseType,
                ExpenseCategory = s.ExpenseCategory,
                VoucherNumber = s.VoucherNumber,
                ExpenseCost = s.ExpenseCost,
                ExpenseNote = s.ExpenseNote,
                ShippingCost = s.ShippingCost
                
               
            }).ToList();
            
            return new PagedResponse<List<ExpenseDto>?>(
                result,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<ExpenseDto>?>(null, 500, "It was not possible to consult all Expense");
        }
    }
}