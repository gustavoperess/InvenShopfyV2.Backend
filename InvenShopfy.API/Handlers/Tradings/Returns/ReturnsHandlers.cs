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
                ReturnDate = request.ReturnDate,
                BillerName = request.BillerName,
                TotalAmount = request.TotalAmount,
                CustomerName = request.CustomerName,
                WarehouseName = request.WarehouseName,
                RemarkStatus = request.Remark,
                ReturnNote = request.ReturnNote,
                ReferenceNumber = request.ReferenceNumber,
            };
            
            // remove item from sales.
            var findSalesByReferenceNumber = await context.Sales.FirstOrDefaultAsync(x => x.ReferenceNumber == request.ReferenceNumber);
            if (findSalesByReferenceNumber != null)
            {
                context.Sales.Remove(findSalesByReferenceNumber);
            }
     
            
            await context.SaleReturns.AddAsync(saleReturn);
            await context.SaveChangesAsync();
            return new Response<SaleReturn?>(saleReturn, 201, "saleReturn created successfully");
        }
        catch
        {
            return new Response<SaleReturn?>(null, 500, "It was not possible to create a new Product");

        }
    }
    
    
    public async Task<Response<List<SalesReturnByReturnNumber>?>> GetSalesPartialByReferenceNumberAsync(GetSalesReturnByNumberRequest request)
    {
        try
        {
            var returns = await context.Sales
                .AsNoTracking()
                .Where(x => EF.Functions.ILike(x.ReferenceNumber, $"%{request.ReferenceNumber}%") && x.UserId == request.UserId)
                .Select(g => new SalesReturnByReturnNumber
                {
                    Id = g.Id,
                    TotalAmount = g.TotalAmount,
                    WarehouseName = g.Warehouse.WarehouseName,
                    CustomerName = g.Customer.Name,
                    BillerName= g.Biller.Name,
                    ReferenceNumber = g.ReferenceNumber,
                }).OrderBy(x => x.CustomerName)
                .ToListAsync();
            
            if (returns.Count == 0)
            {
                return new PagedResponse<List<SalesReturnByReturnNumber>?>(new List<SalesReturnByReturnNumber>(), 200, "No returns found");
            }
            
            return new PagedResponse<List<SalesReturnByReturnNumber>?>(returns);
    
        }
        catch
        {
            return new PagedResponse<List<SalesReturnByReturnNumber>?>(null, 500, "It was not possible to consult all returns");
        }
    }

    public async Task<PagedResponse<List<SaleReturn>?>> GetAllSalesReturnAsync(GetAllSalesReturnsRequest request)
    {
        try
        {
            var query = context.SaleReturns
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.ReturnDate);
            
            var salesReturn = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<SaleReturn>?>(
                salesReturn,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<SaleReturn>?>(null, 500, "It was not possible to consult all salesReturns");
        }
    }
    
}