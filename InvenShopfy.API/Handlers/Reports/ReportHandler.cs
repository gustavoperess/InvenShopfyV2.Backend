using InvenShopfy.API.Common;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Common.Extension;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Models.Reports;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;

using Microsoft.EntityFrameworkCore;


namespace InvenShopfy.API.Handlers.Reports;

public class ReportHandler(AppDbContext context) : IReportHandler

{
    public async Task<PagedResponse<List<SaleReport>?>> GetSalesReportAsync(GetSalesReportRequest request)
    {
        try
        {
            var datetimeHandler = new DateTimeHandler();
            if (request.DateRange != null && request.StartDate == null && request.EndDate == null)
            {
                (request.StartDate, request.EndDate) = datetimeHandler.GetDateRange(request.DateRange);
                
            }  
            else
            {
                request.StartDate ??= DateOnly.FromDateTime(DateTime.Now).GetFirstDayOfYear();
                request.EndDate ??= DateOnly.FromDateTime(DateTime.Now).GetLastDayOfMonth();
            }

        }
        catch
        {
            return new PagedResponse<List<SaleReport>?>(null, 500, "Not possible to determine the start or end date");
        }
        try
        {
            
            var query = context
                .Sales
                .AsNoTracking()
                .Where(x =>
                    x.SaleDate >= request.StartDate &&
                    x.SaleDate <= request.EndDate &&
                    x.UserId == request.UserId)
                .Join(context.Users,
                    ul => ul.BillerId,
                    ur => ur.Id,
                    (sale, user) => new {user, sale})
                .GroupBy(x => new { x.sale.BillerId, x.user.Name })
                .Select(g => new
                {
                    g.Key.BillerId,
                    BillerName = g.Key.Name,
                    TotalQuantitySold = g.Count(),
                    TotalProfit = g.Sum(x => x.sale.ProfitAmount),
                    TotalTaxPaid = g.Sum(x => x.sale.TaxAmount),
                    TotalAmount = g.Sum(x => x.sale.TotalAmount),
                }).OrderByDescending(x => x.TotalAmount);
            var count = await query.CountAsync();
            var sale = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = sale.Select(s => new SaleReport
            {
                BillerId = s.BillerId,
                Name = s.BillerName,
                TotalProfit = s.TotalProfit,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalTaxPaid = s.TotalTaxPaid,
                TotalQuantitySold = s.TotalQuantitySold,
                TotalAmount = s.TotalAmount,
            }).ToList();

            return new PagedResponse<List<SaleReport>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<SaleReport>?>(null, 500, "It was not possible to consult all sale");
        }
    }
    
}