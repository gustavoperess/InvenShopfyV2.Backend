using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Tradings.SalesReturn;
using InvenShopfy.Core.Responses;
using InvenShopfy.Core.Models.Tradings.SalesReturn;
using InvenShopfy.Core.Requests.Tradings.SalesReturn;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Tradings.Returns;

public class ReturnsHandlers(AppDbContext context) : ISalesReturnHandler

{
    public async Task<Response<SaleReturn?>> CreateSalesReturnAsync(CreateSalesReturnRequest request)
    {
        try
        {
            var saleReturn = new SaleReturn
            {
                UserId = request.UserId,
                ReturnDate = DateOnly.FromDateTime(DateTime.Now),
                BillerId = request.BillerId,
                TotalAmount = request.TotalAmount,
                CustomerId = request.CustomerId,
                RemarkStatus = request.Remark,
                ReturnNote = request.ReturnNote,
                ReferenceNumber = request.ReferenceNumber,
            };
            await context.SaleReturns.AddAsync(saleReturn);
            await context.SaveChangesAsync();
            return new Response<SaleReturn?>(saleReturn, 201, "saleReturn created successfully");
        }
        catch
        {
            return new Response<SaleReturn?>(null, 500, "It was not possible to create a new Product");

        }
    }
    
    
    public async Task<Response<List<SalesReturnByName>?>> GetSalesPartialByCustomerNameAsync(GetSalesReturnByCustomerName request)
    {
        try
        {
            var returns = await context.SaleReturns
                .AsNoTracking()
                .Include(p => p.Customer)
                .Include(p => p.Biller)
                .Include(p => p.Warehouse)
                .Where(x => EF.Functions.ILike(x.Customer.Name, $"%{request.CustomerName}%") && x.UserId == request.UserId)
                .Select(g => new SalesReturnByName
                {
                    Id = g.Id,
                    TotalAmount = g.TotalAmount,
                    WarehouseName = g.Warehouse.WarehouseName,
                    CustomerName = g.Customer.Name,
                    BillerName= g.Biller.Name,
                    ReturnNote = g.ReturnNote,
                    ReturnDate = g.ReturnDate,
                    ReferenceNumber = g.ReferenceNumber,
                    Remark = g.RemarkStatus
                    
                }).OrderBy(x => x.CustomerName)
                .ToListAsync();
            
            if (returns.Count == 0)
            {
                return new PagedResponse<List<SalesReturnByName>?>(new List<SalesReturnByName>(), 200, "No returns found");
            }
            
            return new PagedResponse<List<SalesReturnByName>?>(returns);
    
        }
        catch
        {
            return new PagedResponse<List<SalesReturnByName>?>(null, 500, "It was not possible to consult all returns");
        }
    }
}