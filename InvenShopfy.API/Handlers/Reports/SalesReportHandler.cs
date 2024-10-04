using InvenShopfy.API.Data;
using InvenShopfy.Core.Common.Extension;
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
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();
        }
        catch
        {
            return new PagedResponse<List<SaleReport>?>(null, 500,
                "Not possible to determine the start or end date");
        }
        
        try
        {
            var query = context
                .Sales
                .AsNoTracking()
                .Include(x => x.Warehouse)
                .Where(x =>
                    x.SaleDate >= request.StartDate &&
                    x.SaleDate <= request.EndDate &&
                    x.UserId == request.UserId)
                .OrderBy(x => x.SaleDate);
            
            var saleReport = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var result = saleReport.Select(s => new Core.Models.Reports.SaleReport
            { 
                Id = s.Id,
               SalesDate = s.SaleDate,
               Warehouse = s.Warehouse.WarehouseName,
               NumberOfProductsSold = s.TotalQuantitySold,
               TotalAmountSold = s.TotalAmount,
               SaleStatus = s.SaleStatus
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