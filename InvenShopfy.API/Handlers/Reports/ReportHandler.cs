using InvenShopfy.API.Common.DateTimeHandler;
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


    public async Task<PagedResponse<List<SupplierReport>?>> GetSupplierReportAsync(GetReportRequest request)
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
            return new PagedResponse<List<SupplierReport>?>(null, 500,
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
                .GroupBy(x => new { x.Supplier.SupplierName, x.Supplier.Id })
                .Select(g => new
                {
                    g.Key.SupplierName,
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

            var result = sale.Select(s => new SupplierReport
            {
                SupplierId = s.Id,
                SupplierName = s.SupplierName,
                NumberOfPurchases = s.NumberOfPurchases,
                TotalAmount = s.TotalAmount,
                NumberOfProductsBought = s.NumberOfProductsBought,
                TotalPaidInTaxes = s.TotalPaidInTaxes,
                TotalPaidInShipping = s.TotalPaidInShipping,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            }).ToList();

            return new PagedResponse<List<SupplierReport>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<SupplierReport>?>(null, 500,
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
                        product.ProductName,
                        product.ProductCode,
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
                    g => new
                    {
                        g.ProductId, g.StockQuantity, g.TaxQuantity, g.PurchaseCount, g.TotalAmountPaid, g.ProductCode
                    }
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
                .GroupBy(x => new { x.Customer.CustomerName, x.CustomerId, x.Customer.RewardPoint })
                .Select(g => new
                {
                    g.Key.CustomerName,
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
                CustomerName = s.CustomerName,
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
                .GroupBy(x => new { x.ExpenseCategoryId, x.ExpenseCategory.MainCategory })
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


    public async Task<PagedResponse<List<WarehouseReport>?>> GetWarehouseReportAsync(GetReportRequest request)
    {
        try
        {
            var query = context.Warehouses
                .AsNoTracking()
                .GroupJoin(
                    context.Purchases,
                    p => p.Id,
                    pp => pp.WarehouseId,
                    (warehouse, purchases) => new
                    {
                        warehouse.Id,
                        warehouse.WarehouseName,
                        StockQuantity = warehouse.QuantityOfItems,
                        TotalAmountBought = purchases.Sum(po => (decimal?)po.TotalAmountBought) ?? 0,
                        TotalQtyOfProductsBought = purchases.Sum(po => (decimal?)po.TotalNumberOfProductsBought) ?? 0
                    }
                )
                .SelectMany(
                    joined => context.Sales
                        .Where(sp => sp.WarehouseId == joined.Id)
                        .DefaultIfEmpty(),
                    (joined, sale) => new
                    {
                        joined.Id,
                        joined.WarehouseName,
                        joined.StockQuantity,
                        joined.TotalAmountBought,
                        joined.TotalQtyOfProductsBought,
                        TotalPaidInShipping = sale != null ? sale.ShippingCost : 0,
                        TotalAmountSold = sale != null ? sale.TotalAmount: 0,
                        TotalProfit = sale != null ? sale.ProfitAmount: 0,
                        TotalNumberOfSales = sale != null ? sale.TotalQuantitySold: 0,
                    
                    }
                )
                .GroupBy(
                    g => new
                    {
                        g.Id, g.TotalAmountBought,g.TotalQtyOfProductsBought, g.WarehouseName, g.StockQuantity,
                    }
                )
                .Select(group => new
                {
                    group.Key.Id,
                    group.Key.TotalAmountBought,
                    group.Key.WarehouseName,
                    group.Key.TotalQtyOfProductsBought,
                    group.Key.StockQuantity,
                    TotalPaidInShipping = group.Sum(x => x.TotalPaidInShipping),
                    TotalAmountSold = group.Sum(x => x.TotalAmountSold),
                    TotalProfit = group.Sum(x => x.TotalProfit),
                    TotalQtyOfProductsSold = group.Sum(x => x.TotalNumberOfSales),
                    TotalNumberOfSales = group.Select(x => x.TotalNumberOfSales).Count(),
                    
                })
                .OrderByDescending(x => x.TotalAmountBought);

            var count = await query.CountAsync();
            var sale = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = sale.Select(s => new WarehouseReport
            {
               Id = s.Id,
               TotalAmountBought = s.TotalAmountBought,
               WarehouseName = s.WarehouseName,
               TotalNumbersOfProductsBought = s.TotalQtyOfProductsBought,
               StockQuantity = s.StockQuantity,
               TotalPaidInShipping = s.TotalPaidInShipping,
               TotalAmountSold = s.TotalAmountSold,
               TotalProfit = s.TotalProfit,
               TotalNumberOfSales = s.TotalNumberOfSales,
               TotalQtyOfProductsSold = s.TotalQtyOfProductsSold
            }).ToList();

            return new PagedResponse<List<WarehouseReport>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<WarehouseReport>?>(null, 500,
                "It was not possible to consult purchase report");
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
            .Join(context.PurchaseProducts,
                ul => ul.Id,
                ur => ur.AddPurchaseId,
                (purchase, purchaseProduct) => new { purchase, purchaseProduct })
            .Select(g => new
            {
                g.purchase.Id,
                g.purchase.PurchaseDate,
                g.purchase.Warehouse.WarehouseName,
                g.purchaseProduct.Product.ProductName,
                g.purchaseProduct.TotalQuantityBoughtPerProduct,
                g.purchaseProduct.TotalPricePaidPerProduct,
                g.purchaseProduct.TotalInTaxPaidPerProduct,
                g.purchaseProduct.PurchaseReferenceNumber,
                g.purchase.Supplier.SupplierName
            }).OrderBy(x => x.PurchaseReferenceNumber);

        var count = await query.CountAsync();
        var reportHandler = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var result = reportHandler
            .Select((s, index) => new PurchaseReport
            {
                TempKey = $"{Guid.NewGuid()}_{index}", 
                Id = s.Id,
                SupplierName = s.SupplierName,
                ProductName = s.ProductName,
                PurchaseDate = s.PurchaseDate,
                TotalQuantityBoughtPerProduct = s.TotalQuantityBoughtPerProduct,
                TotalPricePaidPerProduct = s.TotalPricePaidPerProduct,
                TotalInTaxPaidPerProduct = s.TotalInTaxPaidPerProduct,
                PurchaseReferenceNumber = s.PurchaseReferenceNumber,
                WarehouseName = s.WarehouseName
            })
            .ToList();

        return new PagedResponse<List<PurchaseReport>?>(result, count, request.PageNumber, request.PageSize);
    }
    catch
    {
        return new PagedResponse<List<PurchaseReport>?>(null, 500,
            "It was not possible to consult purchase report");
    }
}

}