using InvenShopfy.API.Data;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Common.Extension;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace InvenShopfy.API.Handlers.Tradings.Sales;

public class SaleHandler : ISalesHandler
{
    private readonly AppDbContext _context;
    private readonly UserManager<CustomUserRequest> _user;

    public SaleHandler(AppDbContext context, [FromServices] UserManager<CustomUserRequest> user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Response<Sale?>> CreateSaleAsync(CreateSalesRequest request)
    {
        try
        {
            var productIds = request.ProductIdPlusQuantity.Keys;
            var availableSaleProducts = await _context.Products.Where(sp => productIds.Contains(sp.Id)).ToListAsync();
            var sale = new Sale
            {
                CustomerId = request.CustomerId,
                WarehouseId = request.WarehouseId,
                BillerId = request.BillerId,
                SaleDate = request.SaleDate,
                ShippingCost = request.ShippingCost,
                StaffNote = request.StaffNote,
                SaleNote = request.SaleNote,
                SaleStatus = request.SaleStatus,
                UserId = request.UserId,
                TotalAmount = request.TotalAmount,
                TaxAmount = request.TaxAmount,
                ProfitAmount = request.ProfitAmount,
                Discount = request.Discount
            };

            var productResponse = sale.AddProductsToSale(request.ProductIdPlusQuantity, availableSaleProducts);
            if (!productResponse.IsSuccess)
            {
                return productResponse;
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new Response<Sale?>(sale, 201, "Sale created successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            return new Response<Sale?>(null, 500, "It was not possible to create a new sale");
        }
    }


    public async Task<Response<Sale?>> UpdateSaleAsync(UpdateSalesRequest request)
    {
        try
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (sale is null)
            {
                return new Response<Sale?>(null, 404, "sale not found");
            }

            sale.CustomerId = request.CustomerId;
            sale.WarehouseId = request.WarehouseId;
            sale.BillerId = request.BillerId;
            sale.ShippingCost = request.ShippingCost;
            sale.StaffNote = request.StafNote;
            sale.SaleNote = request.SaleNote;

            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
            return new Response<Sale?>(sale, message: "sale updated successfully");
        }
        catch
        {
            return new Response<Sale?>(null, 500, "It was not possible to update this sale");
        }
    }

    public async Task<Response<Sale?>> DeleteSaleAsync(DeleteSalesRequest request)
    {
        try
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (sale is null)
            {
                return new Response<Sale?>(null, 404, "sale not found");
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return new Response<Sale?>(sale, message: "sale removed successfully");
        }
        catch
        {
            return new Response<Sale?>(null, 500, "It was not possible to delete this sale");
        }
    }

    public async Task<PagedResponse<List<SaleList>?>> GetSaleByPeriodAsync(GetAllSalesRequest request)
    {
        try
        {
            var query = _context
                .Sales
                .AsNoTracking()
                .Include(s => s.Customer)
                .Include(s => s.Warehouse)
                .Join(_context.Users,
                    ul => ul.BillerId,
                    userinfo => userinfo.Id,
                    (sale, userinfo) => new { sale, userinfo })
                .Where(x => x.sale.UserId == request.UserId)
                .Select(g => new
                {
                    g.sale.Id,
                    g.sale.SaleDate,
                    g.sale.ReferenceNumber,
                    g.sale.SaleStatus,
                    g.sale.TotalAmount,
                    g.sale.Warehouse.WarehouseName,
                    CustomerName = g.sale.Customer.Name,
                    g.sale.TotalQuantitySold,
                    g.sale.Discount,
                    g.sale.TaxAmount,
                    BillerName = g.userinfo.Name,
                })
                .OrderBy(x => x.SaleDate);

            var sale = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            var result = sale.Select(s => new SaleList
            {
                Id = s.Id,
                SaleDate = s.SaleDate,
                ReferenceNumber = s.ReferenceNumber,
                CustomerName = s.CustomerName,
                WarehouseName = s.WarehouseName,
                SaleStatus = s.SaleStatus,
                BillerName = s.BillerName,
                TotalQuantitySold = s.TotalQuantitySold,
                Discount = s.Discount,
                TotalAmount = s.TotalAmount
            }).ToList();

            return new PagedResponse<List<SaleList>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<SaleList>?>(null, 500, "It was not possible to consult all sale");
        }
    }

    public async Task<PagedResponse<List<BestSeller>?>> GetByBestSellerAsync(GetSalesByBestSeller request)
    {
        try
        {
            request.StartDate ??= DateOnly.FromDateTime(DateTime.Now).GetFirstDay();
            request.EndDate ??= DateOnly.FromDateTime(DateTime.Now).GetLastDay();
        }
        catch
        {
            return new PagedResponse<List<BestSeller>?>(null, 500, "Not possible to determine the start or end date");
        }
        
        try
        {
            var query = _context
                .Sales
                .AsNoTracking()
                .Where(x =>
                    x.SaleDate >= request.StartDate &&
                    x.SaleDate <= request.EndDate &&
                    x.UserId == request.UserId)
                .Join(_context.Users,
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

            var result = sale.Select(s => new BestSeller
            {
                BillerId = s.BillerId,
                Name = s.BillerName,
                TotalProfit = s.TotalProfit,
                TotalTaxPaid = s.TotalTaxPaid,
                TotalQuantitySold = s.TotalQuantitySold,
                TotalAmount = s.TotalAmount,
            }).ToList();

            return new PagedResponse<List<BestSeller>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<BestSeller>?>(null, 500, "It was not possible to consult all sale");
        }
    }

    public async Task<PagedResponse<List<MostSoldProduct>?>> GetMostSoldProductAsync(GetMostSoldProduct request)
    {
        try
        {
            request.StartDate ??= DateOnly.FromDateTime(DateTime.Now).GetFirstDay();
            request.EndDate ??= DateOnly.FromDateTime(DateTime.Now).GetLastDay();
        }
        catch
        {
            return new PagedResponse<List<MostSoldProduct>?>(null, 500,
                "Not possible to determine the start or end date");
        }

        try
        {
            var query = _context
                .SaleProducts
                .AsNoTracking()
                .Include(x => x.Sale)
                .Include(x => x.Product)
                .Where(x =>
                    x.Sale.SaleDate >= request.StartDate &&
                    x.Sale.SaleDate <= request.EndDate &&
                    x.Sale.UserId == request.UserId)
                .GroupBy(x => new { x.ProductId, x.Product.Title, x.Product.ProductCode })
                .Select(g => new
                {
                    Id = g.Key.ProductId,
                    g.Key.ProductCode,
                    ProductName = g.Key.Title,
                    TotalQuantitySoldPerProduct = g.Sum(x => x.TotalQuantitySoldPerProduct),
                }).OrderByDescending(x => x.TotalQuantitySoldPerProduct).Take(5);

            var sale = await query.ToListAsync();

            var result = sale.Select(s => new MostSoldProduct
            {
                Id = s.Id,
                ProductCode = s.ProductCode,
                ProductName = s.ProductName,
                TotalQuantitySoldPerProduct = s.TotalQuantitySoldPerProduct,
            }).ToList();

            return new PagedResponse<List<MostSoldProduct>?>(
                result,
                result.Count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<MostSoldProduct>?>(null, 500, "It was not possible to consult all sale");
        }
    }

    public async Task<Response<decimal?>> GetTotalAmountSalesRequestAsync()
    {
        try
        {
            var totalSalesAmount = await _context.Sales.AsNoTracking().SumAsync(x => x.TotalAmount);
            return new Response<decimal?>(totalSalesAmount, 200, "Total sales amount retrieved successfully");
        }
        catch
        {
            return new Response<decimal?>(null, 500, "It was not possible to consult the total sale");
        }
    }


    public async Task<Response<List<SalePerProduct>?>> GetSalesBySaleIdAsync(GetSalesBySaleIdRequest request)
    {
        try
        {
            var query = _context
                .SaleProducts
                .AsNoTracking()
                .Include(x => x.Sale)
                .Include(x => x.Product)
                .Join(_context.Users,
                    ul => ul.Sale.BillerId,
                    userInfo => userInfo.Id,
                    (sale, userInfo) => new { sale, userInfo })
                .Where(x => x.sale.Sale.UserId == request.UserId && x.sale.SaleId == request.SaleId)
                .GroupBy(x => new
                {
                    x.sale.ProductId, x.sale.Product.Title, x.sale.ReferenceNumber, x.sale.TotalPricePerProduct,
                    x.sale.Sale.Discount,
                    x.sale.TotalQuantitySoldPerProduct, x.sale.Product.Unit.ShortName, x.sale.Sale.TotalAmount,
                    x.sale.Product.Price, x.sale.Sale.ShippingCost,
                    x.sale.Sale.SaleNote, x.sale.Sale.StaffNote, x.userInfo.Name, x.userInfo.Email
                })
                .Select(g => new
                {
                    Id = g.Key.ProductId,
                    ProductPrice = g.Key.Price,
                    g.Key.TotalAmount,
                    g.Key.ReferenceNumber,
                    ProductName = g.Key.Title,
                    g.Key.Discount,
                    g.Key.TotalPricePerProduct,
                    g.Key.TotalQuantitySoldPerProduct,
                    UnitShortName = g.Key.ShortName,
                    g.Key.ShippingCost,
                    g.Key.SaleNote,
                    g.Key.StaffNote,
                    BillerName = g.Key.Name,
                    BillerEmail = g.Key.Email
                });

            var sale = await query.ToListAsync();

            var result = sale.Select(s => new SalePerProduct
            {
                TotalAmount = s.TotalAmount,
                ProductPrice = s.ProductPrice,
                ProductId = s.Id,
                ProductName = s.ProductName,
                UnitShortName = s.UnitShortName,
                ReferenceNumber = s.ReferenceNumber,
                TotalPricePerProduct = s.TotalPricePerProduct,
                TotalQuantitySoldPerProduct = s.TotalQuantitySoldPerProduct,
                Discount = s.Discount,
                ShippingCost = s.ShippingCost,
                SaleNote = s.SaleNote,
                StaffNote = s.StaffNote,
                BillerName = s.BillerName,
                BillerEmail = s.BillerEmail
            }).ToList();

            if (result.Count == 0)
            {
                return new Response<List<SalePerProduct>?>(result, 400, "No item found with this Id");
            }

            return new Response<List<SalePerProduct>?>(result, 200, "Items retrived Successfully");
        }
        catch
        {
            return new Response<List<SalePerProduct>?>(null, 500, "It was not possible to consult all sale");
        }
    }


    public async Task<Response<List<SallerDashboard>?>> GetSaleStatusDashboardAsync(GetAllSalesRequest request)
    {
        try
        {
            var query = _context
                .Sales
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .Select(x => new SallerDashboard
                {
                    Id = x.Id,
                    SaleDate = x.SaleDate,
                    ReferenceNumber = x.ReferenceNumber,
                    Customer = x.Customer.Name,
                    SaleStatus = x.SaleStatus,
                    TotalAmount = x.TotalAmount,
                    TotalQuantitySold = x.TotalQuantitySold
                })
                .OrderByDescending(x => x.SaleDate).Take(10);

            var sale = await query.ToListAsync();
            return new Response<List<SallerDashboard>?>(sale, 201, "Sales returned successfully");
        }
        catch
        {
            return new Response<List<SallerDashboard>?>(null, 500, "It was not possible to consult all Sales");
        }
    }

    public async Task<Response<decimal>> GetTotalProfitDashboardAsync()
    {
        try
        {
            var query = await _context.Sales.AsNoTracking().SumAsync(x => x.ProfitAmount);
            return new Response<decimal>(query, 200, "Total Gross profit returned succesfully");
        }
        catch
        {
            return new Response<decimal>(0, 500, "It was not possible to get the total profit amount");
        }
    }

    public async Task<Response<Sale?>> GetSalesBySellerAsync(GetSalesBySeller request)
    {
        try
        {
            var sale = await _context.Sales.AsNoTracking()
                .FirstOrDefaultAsync(x => x.BillerId == request.BillerId && x.UserId == request.UserId);

            if (sale is null)
            {
                return new Response<Sale?>(null, 404, "sale not found");
            }

            return new Response<Sale?>(sale);
        }
        catch
        {
            return new Response<Sale?>(null, 500, "It was not possible to find this sale");
        }
    }
}