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
    public async Task<PagedResponse<List<SaleReport>?>> GetSalesReportAsync(GetReportRequest request)
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
                    (sale, user) => new { user, sale })
                .GroupBy(x => new { x.sale.BillerId, x.user.Name })
                .Select(g => new
                {
                    g.Key.BillerId,
                    BillerName = g.Key.Name,
                    TotalQuantitySold = g.Count(),
                    TotalProfit = g.Sum(x => x.sale.ProfitAmount),
                    TotalTaxPaid = g.Sum(x => x.sale.TaxAmount),
                    TotalAmount = g.Sum(x => x.sale.TotalAmount),
                    TotalShippingPaid = g.Sum(x => x.sale.ShippingCost)
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
                TotalShippingPaid = s.TotalShippingPaid
            }).ToList();

            return new PagedResponse<List<SaleReport>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<SaleReport>?>(null, 500, "It was not possible to consult all sale");
        }
    }


    public async Task<PagedResponse<List<PurchaseReport>?>> GetPurchaseReportAsync(GetReportRequest request)
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
            return new PagedResponse<List<PurchaseReport>?>(null, 500,
                "Not possible to determine the start or end date");
        }

        try
        {
            var query = context
                .Purchases
                .AsNoTracking()
                .Where(x =>
                    x.PurchaseDate >= request.StartDate &&
                    x.PurchaseDate <= request.EndDate &&
                    x.UserId == request.UserId)
                .Include(x => x.Supplier)
                .GroupBy(x => new { x.Supplier.Name, x.Supplier.Id })
                .Select(g => new
                {
                    g.Key.Name,
                    g.Key.Id,
                    NumberOfPurchases = g.Count(),
                    TotalAmount = g.Sum(x => x.TotalAmountBought),
                    NumberOfProductsBought = g.Sum(x => x.TotalNumberOfProductsBought),
                    TotalPaidInTaxes = g.Sum(x => x.TotalTax),
                    TotalPaidInShipping = g.Sum(x => x.ShippingCost)
                }).OrderByDescending(x => x.NumberOfPurchases);
            var count = await query.CountAsync();
            var sale = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = sale.Select(s => new PurchaseReport
            {
                SupplierId = s.Id,
                SupplierName = s.Name,
                NumberOfPurchases = s.NumberOfPurchases,
                TotalAmount = s.TotalAmount,
                NumberOfProductsBought = s.NumberOfProductsBought,
                TotalPaidInTaxes = s.TotalPaidInTaxes,
                TotalPaidInShipping = s.TotalPaidInShipping,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            }).ToList();

            return new PagedResponse<List<PurchaseReport>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<PurchaseReport>?>(null, 500,
                "It was not possible to consult purchase report");
        }
    }


    public async Task<PagedResponse<List<ProductReport>?>> GetProductReportAsync(GetReportRequest request)
    {
        try
        {
            var query = context.Products
                .AsNoTracking()
                .GroupJoin(
                    context.PurchaseProducts,
                    p => p.Id,
                    pp => pp.ProductId,
                    (product, purchaseProducts) => new
                    {
                        ProductId = product.Id,
                        ProductName = product.Title,
                        ProductCode = product.ProductCode,
                        product.StockQuantity,
                        PurchaseCount = purchaseProducts.Sum(po => (int?)po.TotalQuantityBoughtPerProduct) ?? 0,
                        TaxQuantity = purchaseProducts.Sum(po => (decimal?)po.TotalInTaxPaidPerProduct) ?? 0,
                        TotalAmountPaid = purchaseProducts.Sum(po => (decimal?)po.TotalPricePaidPerProduct) ?? 0
                    }
                )
                .SelectMany(
                    joined => context.SaleProducts
                        .Where(sp => sp.ProductId == joined.ProductId)
                        .DefaultIfEmpty(), 
                    (joined, saleProduct) => new
                    {
                        joined.ProductId,
                        joined.ProductName,
                        joined.StockQuantity,
                        joined.PurchaseCount,
                        joined.TaxQuantity,
                        joined.TotalAmountPaid,
                        joined.ProductCode,
                        SaleQuantity = saleProduct != null ? saleProduct.TotalQuantitySoldPerProduct : 0,
                        SaleRevenue = saleProduct != null ? saleProduct.TotalPricePerProduct : 0
                    }
                )
                .GroupBy(
                    g => new { g.ProductId, g.StockQuantity, g.TaxQuantity, g.PurchaseCount, g.TotalAmountPaid, g.ProductCode }
                )
                .Select(group => new
                {
                    group.Key.ProductId,
                    group.Key.StockQuantity,
                    group.Key.TaxQuantity,
                    group.Key.PurchaseCount,
                    group.Key.TotalAmountPaid,
                    group.Key.ProductCode,
                    ProductName = group.Select(g => g.ProductName).FirstOrDefault(),
                    TotalSaleQuantity = group.Sum(x => x.SaleQuantity),
                    TotalRevenue = group.Sum(x => x.SaleRevenue)
                })
                .OrderByDescending(x => x.TotalRevenue);

            var count = await query.CountAsync();
            var sale = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = sale.Select(s => new ProductReport
            {
                StockQuantity = s.StockQuantity,
                ProductCode = s.ProductCode,
                ProductName = s.ProductName,
                ProductId = s.ProductId,
                TotalRevenue = s.TotalRevenue,
                TotalQuantityBought = s.PurchaseCount,
                TotalAmountPaid = s.TotalAmountPaid,
                TotalPaidInTaxes = s.TaxQuantity,
                TotalQuantitySold = s.TotalSaleQuantity,
            }).ToList();

            return new PagedResponse<List<ProductReport>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<ProductReport>?>(null, 500, "It was not possible to consult product report");
        }
    }
    
     public async Task<PagedResponse<List<CustomerReport>?>> GetCustomerReportAsync(GetReportRequest request)
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
            return new PagedResponse<List<CustomerReport>?>(null, 500,
                "Not possible to determine the start or end date");
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
                .Include(x => x.Customer)
                .GroupBy(x => new { x.Customer.Name , x.CustomerId, x.Customer.RewardPoint})
                .Select(g => new
                {
                    g.Key.Name,
                    g.Key.CustomerId,
                    g.Key.RewardPoint,
                    NumberOfPurchases = g.Count(),
                    TotalPaidInShipping = g.Sum(x => x.ShippingCost),
                    NumberOfProductsBought = g.Sum(x => x.TotalQuantitySold),
                    TotalAmount = g.Sum(x => x.TotalAmount),
                    TotalProfit = g.Sum(x => x.ProfitAmount),
                    TotalPaidInTaxes = g.Sum(x => x.TaxAmount),
                    LastPurchase = g.Max(x => x.SaleDate)
                    
                }).OrderByDescending(x => x.NumberOfPurchases);
            var count = await query.CountAsync();
            var sale = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            var result = sale.Select(s => new CustomerReport
            {
                Id = s.CustomerId,
                RewardPoints = s.RewardPoint,
                CustomerName = s.Name,
                NumberOfPurchases = s.NumberOfPurchases,
                TotalAmount = s.TotalAmount,
                NumberOfProductsBought = s.NumberOfProductsBought,
                TotalPaidInTaxes = s.TotalPaidInTaxes,
                TotalProfit = s.TotalProfit,
                TotalPaidInShipping = s.TotalPaidInShipping,
                LastPurchase = s.LastPurchase,
            }).ToList();

            return new PagedResponse<List<CustomerReport>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<CustomerReport>?>(null, 500,
                "It was not possible to consult purchase report");
        }
    }
     
     public async Task<PagedResponse<List<ExpenseReport>?>> GetExpenseReportAsync(GetReportRequest request)
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
            return new PagedResponse<List<ExpenseReport>?>(null, 500,
                "Not possible to determine the start or end date");
        }

        try
        {
            var query = context
                .Expenses
                .AsNoTracking()
                .Where(x =>
                    x.Date >= request.StartDate &&
                    x.Date <= request.EndDate &&
                    x.UserId == request.UserId)
                .Include(x => x.ExpenseCategory)
                .GroupBy(x => new { x.ExpenseCategoryId, x.ExpenseCategory.MainCategory})
                .Select(g => new
                {
                    Name = g.Key.MainCategory,
                    Id = g.Key.ExpenseCategoryId,
                    NumberOfTimesUsed = g.Count(),
                    TotalCost = g.Sum(x => x.ExpenseCost),
                    ShippingCost = g.Sum(x => x.ShippingCost),
                    
                }).OrderByDescending(x => x.TotalCost);
            var count = await query.CountAsync();
            var sale = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            var result = sale.Select(s => new ExpenseReport
            {
                Id = s.Id,
                Name = s.Name,
                NumberOfTimesUsed = s.NumberOfTimesUsed,
                TotalCost = s.TotalCost,
                ShippingCost = s.ShippingCost
            }).ToList();

            return new PagedResponse<List<ExpenseReport>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<ExpenseReport>?>(null, 500,
                "It was not possible to consult purchase report");
        }
    }
}

