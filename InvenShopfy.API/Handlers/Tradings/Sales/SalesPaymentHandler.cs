using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Expenses.ExpensePayment;
using InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Requests.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

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
                    return new Response<SalesPayment?>(null, 500, "This Sale has already been paid");
                  
                }
                expense.SaleStatus = "Paid";
                context.Sales.Update(expense);
            }
            var expensePayment = new SalesPayment
            {
                UserId = request.UserId,
                SalesId = request.SalesId,
                Date = request.Date,
                CardNumber = request.CardNumber,
                SalesNote= request.SalesNote,
            };
            
            await context.SalesPayments.AddAsync(expensePayment);
            await context.SaveChangesAsync();

            return new Response<SalesPayment?>(expensePayment, 201, "Sales Payment created successfully");
        }
        catch
        {
            return new Response<SalesPayment?>(null, 500, "It was not possible to create a new payment Expense");
        }
    }

    public async Task<Response<SalesPaymentDto?>> GetSalesPaymentDetailsByIdAsync(GetSalesPaymentByIdRequest request)
    {
        try
        {
            var paidItem = await context.SalesPayments
                .Include(x => x.Sales)
                .Include(x => x.Sales.Customer)
                .AsNoTracking().
                FirstOrDefaultAsync(x => x.SalesId == request.Id);
            if (paidItem is null)
            {
                return new Response<SalesPaymentDto?>(null, 404, "It was not possible to find this ExpensePayment");
            
            }
            var result = new SalesPaymentDto
            {
                Id = paidItem.Id,
                ReferenceNumber = paidItem.Sales.ReferenceNumber,
                CustomerName = paidItem.Sales.Customer.CustomerName,
                Date = paidItem.Sales.SaleDate,
                TotalAmount = paidItem.Sales.TotalAmount,
                PaymentType = paidItem.PaymentType,
                CardNumber = $"**** **** **** {paidItem.CardNumber.Substring(paidItem.CardNumber.Length - 4)}"
            };
            return new Response<SalesPaymentDto?>(result, 201, "Payment Expense retuned successfully");
        }
        catch
        {
            return new Response<SalesPaymentDto?>(null, 500, "It was not possible to find this Expense");

        }
        
    }
    
    public async Task<Response<SalesPaymentDto?>> GetSalesPaymentByIdAsync(GetSalesPaymentByIdRequest request)
    {
        try
        {
            var sales = await context.Sales
                .Include(x => x.Warehouse)
                .Include(x => x.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
    
            if (sales is null)
            {
                return new Response<SalesPaymentDto?>(null, 404, "It was not possible to find this ExpensePayment");
            }
            
            var result = new SalesPaymentDto
            {
                Id = sales.Id,
                ReferenceNumber = sales.ReferenceNumber,
                TotalAmount = sales.TotalAmount,
                WarehouseName = sales.Warehouse.WarehouseName,
                CustomerName = sales.Customer.CustomerName
            };
            return new Response<SalesPaymentDto?>(result, 201, "Payment Expense retuned successfully");
        }
        catch
        {
            return new Response<SalesPaymentDto?>(null, 500, "It was not possible to find this Expense");
        }
    }
    
    
    
}