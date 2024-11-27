using System.Globalization;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Handlers.Notifications;
using InvenShopfy.Core.Models.Expenses.Expense;
using InvenShopfy.Core.Models.Expenses.ExpenseDto;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Notifications;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Expenses;

public class ExpenseHandler : IExpenseHandler
{
    private readonly AppDbContext _context;
    private readonly INotificationHandler _notificationHandler;

    public ExpenseHandler(AppDbContext context, INotificationHandler notificationHandler)
    {
        _context = context;
        _notificationHandler = notificationHandler;
    }

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
                ExpenseCost = request.ExpenseCost,
                ExpenseNote = request.ExpenseNote,
                ExpenseStatus = request.ExpenseStatus ?? "Incompleted",
                ExpenseDescription = request.ExpenseDescription,
                ShippingCost = request.ShippingCost
            };
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();

            var notificationRequest = new CreateNotificationsRequest
            {
                NotificationTitle = $"New Expense created: {expense.ExpenseType}",
                Urgency = false,
                From = "System-Expenses",
                Image = null,
                UserId = request.UserId,
                Href = "/expense/expenselist",
            };
            await _notificationHandler.CreateNotificationAsync(notificationRequest);

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
            var expense =
                await _context.Expenses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (expense is null)
            {
                return new Response<Expense?>(null, 404, "Expense not found");
            }

            expense.WarehouseId = request.WarehouseId;
            expense.ExpenseType = request.ExpenseType;
            expense.ExpenseCategoryId = request.ExpenseCategoryId;
            expense.ExpenseCost = request.ExpenseCost;
            expense.ExpenseNote = request.ExpenseNote;
            expense.ExpenseDescription = request.ExpenseDescription;
            expense.ShippingCost = request.ShippingCost;

            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
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
            var expense =
                await _context.Expenses.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (expense is null)
            {
                return new Response<Expense?>(null, 404, "Expense  not found");
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return new Response<Expense?>(expense, message: "Expense  removed successfully");
        }
        catch
        {
            return new Response<Expense?>(null, 500, "It was not possible to delete this Expense");
        }
    }

    public async Task<Response<ExpenseUpdate?>> GetExpenseByIdAsync(GetExpenseByIdRequest request)
    {
        try
        {
            var expense = await _context.Expenses
                .Include(x => x.ExpenseCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (expense is null)
            {
                return new Response<ExpenseUpdate?>(null, 404, "Expense  not found");
            }
            
            var query = new ExpenseUpdate
            {
                Id = expense.Id,
                ExpenseCost = expense.ExpenseCost,
                VoucherNumber = expense.VoucherNumber,
                ExpenseCategory = expense.ExpenseCategory.MainCategory,
                PaymentStatus = expense.ExpenseStatus
            };
            
            return new Response<ExpenseUpdate?>(query);
        }
        catch
        {
            return new Response<ExpenseUpdate?>(null, 500, "It was not possible to find this Expense");
        }
    }

    public async Task<PagedResponse<List<ExpenseDto>?>> GetExpenseByPeriodAsync(GetAllExpensesRequest request)
    {
        try
        {
            var query = _context
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
                    ExpenseCategory = g.ExpenseCategory.MainCategory,
                    g.VoucherNumber,
                    g.ExpenseCost,
                    g.ExpenseStatus,
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
                WarehouseName = s.WarehouseName,
                ExpenseType = s.ExpenseType,
                ExpenseCategory = s.ExpenseCategory,
                VoucherNumber = s.VoucherNumber,
                ExpenseStatus = s.ExpenseStatus,
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

    public async Task<Response<List<ExpenseDashboard>?>> GetExpenseStatusDashboardAsync(GetAllExpensesRequest request)
    {
        try
        {
            var query = _context
                .Expenses
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .Select(x => new ExpenseDashboard
                {
                    Id = x.Id,
                    Date = x.Date,
                    VoucherNumber = x.VoucherNumber,
                    ExpenseStatus = x.ExpenseStatus,
                    ExpenseDescription = x.ExpenseDescription,
                    ExpenseCategory = x.ExpenseCategory.MainCategory,
                    ExpenseType = x.ExpenseType,
                    ExpenseCost = x.ExpenseCost,
                })
                .OrderByDescending(x => x.Date).Take(10);

            var sale = await query.ToListAsync();
            return new Response<List<ExpenseDashboard>?>(sale, 201, "Expenses returned successfully");
        }
        catch
        {
            return new Response<List<ExpenseDashboard>?>(null, 500, "It was not possible to consult all Expenses");
        }
    }

    public async Task<Response<decimal?>> GetExpenseTotalAmountAsync()
    {
        try
        {
            var totalExpense = await _context
                .Expenses
                .AsNoTracking()
                .SumAsync(x => x.ExpenseCost);

            return new Response<decimal?>(totalExpense, 201, "Expenses returned successfully");
        }
        catch
        {
            return new Response<decimal?>(0, 400, "It was not possible to consult all Expenses");
        }
    }
    
    public async Task<PagedResponse<List<ExpenseDto>?>> GetExpenseByPartialNumberAsync(GetExpenseByNumberRequest request)
    {
        try
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var products = await _context.Expenses
                .AsNoTracking()
                .Where(x => EF.Functions.ILike(x.VoucherNumber, $"%{request.ExpenseNumber}%") && x.UserId == request.UserId)
                .Select(x => new ExpenseDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    VoucherNumber = x.VoucherNumber,
                    ExpenseStatus = x.ExpenseStatus,
                    ExpenseDescription = x.ExpenseDescription,
                    ExpenseCategory = x.ExpenseCategory.MainCategory,
                    ExpenseType = x.ExpenseType,
                    ExpenseCost = x.ExpenseCost,
                    
                }).ToListAsync();
            
            if (products.Count == 0)
            {
                return new PagedResponse<List<ExpenseDto>?>(new List<ExpenseDto>(), 200, "No products found");
            }
            
            return new PagedResponse<List<ExpenseDto>?>(products);
    
        }
        catch
        {
            return new PagedResponse<List<ExpenseDto>?>(null, 500, "It was not possible to consult all Products");
        }
    }
}