using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn.Dto;
using InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Tradings.Returns;

public class SalesReturnHandlers(AppDbContext context) : ISalesReturnHandler

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
            var findSalesByReferenceNumber =
                await context.Sales.FirstOrDefaultAsync(x => x.ReferenceNumber == request.ReferenceNumber);
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


    public async Task<Response<List<SalesReturnByReturnNumber>?>> GetSalesPartialByReferenceNumberAsync(
        GetSalesReturnByNumberRequest request)
    {
        try
        {
            var returns = await context.Sales
                .AsNoTracking()
                .Where(x => EF.Functions.ILike(x.ReferenceNumber, $"%{request.ReferenceNumber}%") &&
                            x.UserId == request.UserId)
                .Select(g => new SalesReturnByReturnNumber
                {
                    Id = g.Id,
                    TotalAmount = g.TotalAmount,
                    WarehouseName = g.Warehouse.WarehouseName,
                    CustomerName = g.Customer.Name,
                    BillerName = g.Biller.Name,
                    ReferenceNumber = g.ReferenceNumber,
                }).OrderBy(x => x.CustomerName)
                .ToListAsync();

            if (returns.Count == 0)
            {
                return new PagedResponse<List<SalesReturnByReturnNumber>?>(new List<SalesReturnByReturnNumber>(), 200,
                    "No returns found");
            }

            return new PagedResponse<List<SalesReturnByReturnNumber>?>(returns);
        }
        catch
        {
            return new PagedResponse<List<SalesReturnByReturnNumber>?>(null, 500,
                "It was not possible to consult all returns");
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

    public async Task<Response<SaleReturn?>> DeleteSalesReturnAsync(DeleteSalesReturnRequest request)
    {
        try
        {
            var saleReturn =
                await context.SaleReturns.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (saleReturn is null)
            {
                return new Response<SaleReturn?>(null, 404, "SaleReturn not found");
            }

            context.SaleReturns.Remove(saleReturn);
            await context.SaveChangesAsync();
            return new Response<SaleReturn?>(saleReturn, message: "saleReturn removed successfully");
        }
        catch
        {
            return new Response<SaleReturn?>(null, 500, "It was not possible to delete this saleReturn");
        }
    }
    

    public async Task<Response<decimal?>> GetTotalSalesReturnAsync(GetAllSalesReturnsRequest request)
    {
        try
        {
            var saleReturn = await context.SaleReturns.AsNoTracking().SumAsync(x => x.TotalAmount);
            
            return new Response<decimal?>(saleReturn, message: "saleReturn returned successfully");
        }
        catch
        {
            return new Response<decimal?>(0, 500, "It was not possible to returned this saleReturn");
        }
    }

    public async Task<Response<List<SalesReturnDashboard>?>> GetSaleReturnDashboardAsync(
        GetAllSalesReturnsRequest request)
    {
        try
        {
            var query = context
                .SaleReturns
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .Select(x => new SalesReturnDashboard
                {
                    Id = x.Id,
                    ReturnDate = x.ReturnDate,
                    ReferenceNumber = x.ReferenceNumber,
                    BillerName = x.BillerName,
                    CustomerName = x.CustomerName,
                    RemarkStatus = x.RemarkStatus,
                    TotalAmount = x.TotalAmount,
                })
                .OrderByDescending(x => x.ReturnDate).Take(10);

            var sale = await query.ToListAsync();
            return new Response<List<SalesReturnDashboard>?>(sale, 201, "Sales returned successfully");
        }
        catch
        {
            return new Response<List<SalesReturnDashboard>?>(null, 500, "It was not possible to consult all Sales");
        }
    }
}