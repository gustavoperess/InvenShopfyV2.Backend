using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Models.Reports;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Reports;

public class SalesReportHandler(AppDbContext context) : ISalesReportHandler

{
    public async Task<PagedResponse<List<SaleReport>?>> GetByPeriodAsync(GetSalesReportRequest request)
    {
        try
        {
            var query = context
                .Sales
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.SaleDate);
            
            var saleReport = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var result = saleReport.Select(s => new Core.Models.Reports.SaleReport
            {
                Id = s.Id,
               
                
            }).ToList();
            
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<SaleReport>?>(
                result,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<SaleReport>?>(null, 500, "It was not possible to consult all Categories");
        }
    }
}