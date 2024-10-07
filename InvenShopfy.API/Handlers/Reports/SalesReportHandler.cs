using InvenShopfy.API.Data;
using InvenShopfy.Core.Common.Extension;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Handlers.Reports.Sales;
using InvenShopfy.Core.Models.Reports;
using InvenShopfy.Core.Requests;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Requests.Reports.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Reports;

public class SalesReportHandler(AppDbContext context) : ISalesReportHandler

{
    public async Task<PagedResponse<List<SaleReport>?>> GetByPeriodAsync(GetSalesReportByDateRequest byDateRequest)
    {
        try
        {
            byDateRequest.StartDate ??= DateOnly.FromDateTime(DateTime.Now).GetFirstDay();
            byDateRequest.EndDate ??= DateOnly.FromDateTime(DateTime.Now).GetLastDay();
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
                    x.SaleDate >= byDateRequest.StartDate &&
                    x.SaleDate <= byDateRequest.EndDate &&
                    x.UserId == byDateRequest.UserId)
                .OrderBy(x => x.SaleDate);
            
            var saleReport = await query
                .Skip((byDateRequest.PageNumber - 1) * byDateRequest.PageSize)
                .Take(byDateRequest.PageSize)
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
                byDateRequest.PageNumber,
                byDateRequest.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<SaleReport>?>(null, 500, "It was not possible to consult all Categories");
        }
    }
    
    
    public async Task<PagedResponse<List<SaleReport>?>> GetByWarehouseNameAsync(GetSalesReportByWarehouseRequest request)
    {
        try
        {
            var query = context.Sales
                .AsNoTracking()
                .Include(x => x.Warehouse)
                .Where(x => x.Warehouse.WarehouseName == request.WarehouseName && x.UserId == request.UserId);
            
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