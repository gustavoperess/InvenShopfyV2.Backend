using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Expenses;

public class ExpenseCategoryHandler (AppDbContext context) : IExpenseCategoryHandler
{
    public async Task<Response<ExpenseCategory?>> CreateExpenseCategoryAsync(CreateExpenseCategoryRequest request)
    {
        try
        {
            var existingExpenseCategory = await context.ExpenseCategories.FirstOrDefaultAsync(c =>
                c.Category == request.Category && c.UserId == request.UserId);
            if (existingExpenseCategory != null)
            {
                foreach (var subcat in request.SubCategory)
                {
                    if (!existingExpenseCategory.SubCategory.Contains(subcat))
                    {
                        existingExpenseCategory.SubCategory.Add(subcat);
                    }
                }
                context.ExpenseCategories.Update(existingExpenseCategory);
                await context.SaveChangesAsync();
            }
            var expenseCategory = new ExpenseCategory
            {
                UserId = request.UserId,
                SubCategory = request.SubCategory,
                Category = request.Category
            };
            await context.ExpenseCategories.AddAsync(expenseCategory);
            await context.SaveChangesAsync();

            return new Response<ExpenseCategory?>(expenseCategory, 201, "expenseCategory created successfully");
            
            
        }
        
        catch
        {
            return new Response<ExpenseCategory?>(null, 500, "It was not possible to create a new expenseCategory");
        }
        
        
    }

    public async Task<Response<ExpenseCategory?>> UpdateExpenseCategoryAsync(UpdateExpenseCategoryRequest request)
    {
        try
        {
            var expenseCategory = await context.ExpenseCategories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (expenseCategory is null)
            {
                return new Response<ExpenseCategory?>(null, 404, "Expense Category not found");
            }
            
            expenseCategory.Category = request.Category;
            expenseCategory.SubCategory = request.SubCategory;
            context.ExpenseCategories.Update(expenseCategory);
            await context.SaveChangesAsync();
            return new Response<ExpenseCategory?>(expenseCategory, message: "Expense Category updated successfully");

        }
        catch 
        {
            return new Response<ExpenseCategory?>(null, 500, "It was not possible to update this Expense Category");
        }
    }

    public async Task<Response<ExpenseCategory?>> DeleteExpenseCategoryAsync(DeleteExpenseCategoryRequest request)
    {
        try
        {
            var expenseCategory = await context.ExpenseCategories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (expenseCategory is null)
            {
                return new Response<ExpenseCategory?>(null, 404, "Expense Category not found");
            }

            context.ExpenseCategories.Remove(expenseCategory);
            await context.SaveChangesAsync();
            return new Response<ExpenseCategory?>(expenseCategory, message: "Expense Category removed successfully");

        }
        catch 
        {
            return new Response<ExpenseCategory?>(null, 500, "It was not possible to delete this Expense Category");
        }
    }
    
    public async Task<Response<ExpenseCategory?>> GetExpenseCategoryByIdAsync(GetExpenseCategoryByIdRequest request)
    {
        try
        {
            var expenseCategory = await context.ExpenseCategories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (expenseCategory is null)
            {
                return new Response<ExpenseCategory?>(null, 404, "Expense Category not found");
            }

            return new Response<ExpenseCategory?>(expenseCategory);

        }
        catch 
        {
            return new Response<ExpenseCategory?>(null, 500, "It was not possible to find this Expense Category");
        }
    }
    public async Task<PagedResponse<List<ExpenseCategory>?>> GetExpenseCategoryByPeriodAsync(GetAllExpensesCategoriesRequest request)
    {
        try
        {
            var query = context
                .ExpenseCategories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Category);
            
            var expenseCategory = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<ExpenseCategory>?>(
                expenseCategory,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<ExpenseCategory>?>(null, 500, "It was not possible to consult all Expense Categories");
        }
    }
}