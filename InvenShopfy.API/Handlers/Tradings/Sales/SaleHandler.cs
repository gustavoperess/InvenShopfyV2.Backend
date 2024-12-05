using System.Globalization;
using InvenShopfy.API.Data;
using InvenShopfy.Core;
using InvenShopfy.Core.Common.Extension;
using InvenShopfy.Core.Handlers.Notifications;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Notifications;
using InvenShopfy.Core.Requests.Tradings.Sales.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;


namespace InvenShopfy.API.Handlers.Tradings.Sales;

public class SaleHandler : ISalesHandler
{
    private readonly AppDbContext _context;
    private readonly INotificationHandler _notificationHandler;

    public SaleHandler(AppDbContext context, INotificationHandler notificationHandler)
    {
        _context = context;
        _notificationHandler = notificationHandler;
    }

    public async Task<Response<Sale?>> CreateSaleAsync(CreateSalesRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new Response<Sale?>(null, 409, $"{Configuration.NotAuthorized} 'create'");
            }

            
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

            var notificationRequest = new CreateNotificationsRequest
            {
                NotificationTitle =
                    $"New Sale Of {request.TotalAmount.ToString("C", CultureInfo.CurrentCulture)} created",
                Urgency = false,
                From = "System-Sales",
                Image = null,
                Href = "/sales/salelist",
            };
            await _notificationHandler.CreateNotificationAsync(notificationRequest);

            return new Response<Sale?>(sale, 201, "Sale created successfully");
        }
        catch
        {
            return new Response<Sale?>(null, 500, "It was not possible to create a new sale");
        }
    }


    public async Task<Response<Sale?>> UpdateSaleAsync(UpdateSalesRequest request)
    {
        try
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id);

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
            if (!request.UserHasPermission)
            {
                return new Response<Sale?>(null, 400, $"{Configuration.NotAuthorized} 'Delete'");
            }
            
            var sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id);

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
            if (!request.UserHasPermission)
            {
                return new PagedResponse<List<SaleList>?>([], 201, $"{Configuration.NotAuthorized}");
            }
            
            var query = _context
                .Sales
                .AsNoTracking()
                .Include(s => s.Customer)
                .Include(s => s.Warehouse)
                .Join(_context.Users,
                    ul => ul.BillerId,
                    userinfo => userinfo.Id,
                    (sale, userinfo) => new { sale, userinfo })
                .Select(g => new
                {
                    g.sale.Id,
                    g.sale.SaleDate,
                    g.sale.ReferenceNumber,
                    g.sale.SaleStatus,
                    g.sale.TotalAmount,
                    g.sale.Warehouse.WarehouseName,
                    g.sale.Customer.CustomerName,
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


    public async Task<PagedResponse<List<MostSoldProduct>?>> GetMostSoldProductAsync(GetMostSoldProduct request)
    {
        try
        {
            request.StartDate ??= DateOnly.FromDateTime(DateTime.Now).GetFirstDayOfMonth();
            request.EndDate ??= DateOnly.FromDateTime(DateTime.Now).GetLastDayOfMonth();
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
                // .Where(x =>
                //     x.Sale.SaleDate >= request.StartDate &&
                //     x.Sale.SaleDate <= request.EndDate)
                .GroupBy(x => new { x.ProductId, Title = x.Product.ProductName, x.Product.ProductCode })
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


    public async Task<Response<List<PosSale>?>> GetSalesBySaleIdForPosSaleAsync(GetSalesBySaleIdRequest request)
    {
        try
        {
            var query = _context
                .SaleProducts
                .AsNoTracking()
                .Include(x => x.Sale)
                .Include(x => x.Product)
                .Include(x => x.Product.Brand)
                .Include(x => x.Product.Category)
                .Join(_context.Users,
                    ul => ul.Sale.BillerId,
                    userInfo => userInfo.Id,
                    (sale, userInfo) => new { sale, userInfo })
                .Where(x => x.sale.SaleId == request.SaleId)
                .GroupBy(x => new
                {
                    //Product
                    x.sale.ProductId,
                    x.sale.Product.ProductName,
                    x.sale.Product.Unit.UnitShortName,
                    x.sale.Product.Featured,
                    x.sale.Product.ProductImage,
                    x.sale.Product.ProductPrice,
                    x.sale.Product.ProductCode,
                    // //productBrand
                    BrandId = x.sale.Product.Brand.Id.ToString(),
                    x.sale.Product.Brand.BrandName,
                    x.sale.Product.Brand.BrandImage,
                    // //productCategory
                    CategoryId = x.sale.Product.Category.Id.ToString(),
                    x.sale.Product.Category.MainCategory,
                    // sale product
                    x.sale.TotalPricePerProduct,
                    x.sale.TotalQuantitySoldPerProduct,
                    x.sale.ReferenceNumber,
                    // sale
                    x.sale.Sale.Discount,
                    x.sale.Sale.TaxAmount,
                    x.sale.Sale.TotalAmount,
                    x.sale.Sale.ShippingCost,
                    x.sale.Sale.ProfitAmount,
                    x.sale.Sale.SaleNote,
                    x.sale.Sale.StaffNote,
                    //user
                    x.userInfo.Name,
                    x.userInfo.Email
                })
                .Select(g => new
                {
                    Id = g.Key.ProductId,
                    g.Key.ProductPrice,
                    g.Key.TotalAmount,
                    g.Key.ReferenceNumber,
                    g.Key.ProductName,
                    g.Key.ProductImage,
                    g.Key.BrandName,
                    g.Key.BrandImage,
                    g.Key.ProductCode,
                    g.Key.TaxAmount,
                    g.Key.MainCategory,
                    g.Key.Discount,
                    g.Key.ProfitAmount,
                    g.Key.TotalPricePerProduct,
                    g.Key.Featured,
                    g.Key.TotalQuantitySoldPerProduct,
                    g.Key.UnitShortName,
                    g.Key.ShippingCost,
                    g.Key.SaleNote,
                    g.Key.BrandId,
                    g.Key.CategoryId,
                    g.Key.StaffNote,
                    BillerName = g.Key.Name,
                    BillerEmail = g.Key.Email
                });

            var sale = await query.ToListAsync();

            var processedBrands = new HashSet<string>(); 
            var processedCategories = new HashSet<string>();

            var result = sale.Select(s =>
            {
                bool isBrandDuplicate = processedBrands.Contains(s.BrandId); 
                bool isCategoryDuplicate = processedCategories.Contains(s.CategoryId);
                processedBrands.Add(s.BrandId);
                processedCategories.Add(s.CategoryId);

                return new PosSale
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
                    TaxAmount = s.TaxAmount,
                    ProductImage = s.ProductImage,
                    CategoryId = isCategoryDuplicate ? null : s.CategoryId,
                    BrandId = isBrandDuplicate ? null : s.BrandId,
                    BrandImage = isBrandDuplicate ? null : s.BrandImage,
                    MainCategory = isCategoryDuplicate ? null : s.MainCategory,
                    BrandName = isBrandDuplicate ? null : s.BrandName,
                    Featured = s.Featured,
                    ProfitAmount = s.ProfitAmount,
                    ShippingCost = s.ShippingCost,
                    SaleNote = s.SaleNote,
                    StaffNote = s.StaffNote,
                    BillerName = s.BillerName,
                    BillerEmail = s.BillerEmail
                };
            }).ToList();

            if (result.Count == 0)
            {
                return new Response<List<PosSale>?>(result, 400, "No item found with this Id");
            }

            return new Response<List<PosSale>?>(result, 200, "Items retrived Successfully");
        }
        catch
        {
            return new Response<List<PosSale>?>(null, 500, "It was not possible to consult items for Post Sale");
        }
    }


    public async Task<Response<List<SalePopUp>?>> GetSalesBySaleIdAsync(GetSalesBySaleIdRequest request)
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
                .Where(x => x.sale.SaleId == request.SaleId)
                .GroupBy(x => new
                {
                    x.sale.ProductId,
                    x.sale.Product.ProductName,
                    x.sale.ReferenceNumber,
                    x.sale.TotalPricePerProduct,
                    x.sale.Sale.Discount,
                    x.sale.Sale.SaleDate,
                    x.sale.Sale.Warehouse.WarehouseName,
                    x.sale.Sale.SaleStatus,
                    x.sale.TotalQuantitySoldPerProduct,
                    x.sale.Product.Unit.UnitShortName,
                    x.sale.Sale.TotalAmount,
                    x.sale.Sale.TaxAmount,
                    x.sale.Product.ProductPrice,
                    x.sale.Sale.ShippingCost,
                    x.sale.Sale.SaleNote,
                    x.sale.Sale.StaffNote,
                    x.sale.Sale.Customer.CustomerName,
                    CustomerEmail = x.sale.Sale.Customer.Email,
                    CustomerPhoneNumber = x.sale.Sale.Customer.PhoneNumber,
                    CustomerAddress = x.sale.Sale.Customer.Address,
                    BillerName = x.userInfo.Name,
                    BillerPhoneNumber = x.userInfo.PhoneNumber,
                    BillerEmail = x.userInfo.Email
                })
                .Select(g => new
                {
                    Id = g.Key.ProductId,
                    g.Key.ProductPrice,
                    g.Key.TotalAmount,
                    g.Key.TaxAmount,
                    g.Key.Discount,
                    g.Key.ReferenceNumber,
                    g.Key.ProductName,
                    g.Key.TotalPricePerProduct,
                    g.Key.WarehouseName,
                    g.Key.SaleStatus,
                    g.Key.TotalQuantitySoldPerProduct,
                    g.Key.UnitShortName,
                    g.Key.ShippingCost,
                    g.Key.SaleNote,
                    g.Key.StaffNote,
                    g.Key.BillerName,
                    g.Key.BillerEmail,
                    g.Key.SaleDate,
                    g.Key.BillerPhoneNumber,
                    g.Key.CustomerPhoneNumber,
                    g.Key.CustomerName,
                    g.Key.CustomerEmail,
                    g.Key.CustomerAddress
                });

            var sale = await query.ToListAsync();

            var result = sale.Select(s => new SalePopUp
            {
                TotalAmount = s.TotalAmount,
                ProductPrice = s.ProductPrice,
                ProductId = s.Id,
                ProductName = s.ProductName,
                TaxAmount = s.TaxAmount,
                UnitShortName = s.UnitShortName,
                ReferenceNumber = s.ReferenceNumber,
                TotalPricePerProduct = s.TotalPricePerProduct,
                TotalQuantitySoldPerProduct = s.TotalQuantitySoldPerProduct,
                Discount = s.Discount,
                CustomerAddress = s.CustomerAddress,
                WarehouseName = s.WarehouseName,
                SaleStatus = s.SaleStatus,
                SaleDate = s.SaleDate,
                BillerPhoneNumber = s.BillerPhoneNumber,
                CustomerEmail = s.CustomerEmail,
                CustomerPhoneNumber = s.BillerPhoneNumber,
                CustomerName = s.CustomerName,
                ShippingCost = s.ShippingCost,
                SaleNote = s.SaleNote,
                StaffNote = s.StaffNote,
                BillerName = s.BillerName,
                BillerEmail = s.BillerEmail
            }).ToList();

            if (result.Count == 0)
            {
                return new Response<List<SalePopUp>?>(result, 400, "No item found with this Id");
            }

            return new Response<List<SalePopUp>?>(result, 200, "Items retrived Successfully");
        }
        catch
        {
            return new Response<List<SalePopUp>?>(null, 500, "It was not possible to consult all sale");
        }
    }

    public async Task<Response<List<SallerDashboard>?>> GetSaleStatusDashboardAsync(GetAllSalesRequest request)
    {
        try
        {
            var query = _context
                .Sales
                .AsNoTracking()
                .Select(x => new SallerDashboard
                {
                    Id = x.Id,
                    SaleDate = x.SaleDate,
                    ReferenceNumber = x.ReferenceNumber,
                    Customer = x.Customer.CustomerName,
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
}