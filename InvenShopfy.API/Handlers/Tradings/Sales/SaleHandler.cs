using InvenShopfy.API.Data;
using InvenShopfy.Core.Common.Extension;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;


namespace InvenShopfy.API.Handlers.Tradings.Sales;
public class SaleHandler(AppDbContext context) : ISalesHandler 
{
    public async Task<Response<Sale?>> CreateAsync(CreateSalesRequest request)
    {
        try
        {
            var productIds = request.ProductIdPlusQuantity.Keys;
            var availableSaleProducts = await context.Products.Where(sp => productIds.Contains(sp.Id)).ToListAsync();
            var sale = new Sale
            {
                CustomerId = request.CustomerId,
                WarehouseId = request.WarehouseId,
                BillerId = request.BillerId,
                SaleDate = request.SaleDate,
                ShippingCost = request.ShippingCost,
                StaffNote = request.StaffNote,
                SaleNote = request.SaleNote,
                PaymentStatus = request.PaymentStatus,
                SaleStatus = request.SaleStatus,
                UserId = request.UserId,
                TotalAmount = request.TotalAmount,
                Discount = request.Discount
            };
            
            var productResponse = sale.AddProductsToSale(request.ProductIdPlusQuantity, availableSaleProducts);
            if (!productResponse.IsSuccess)
            {
                return productResponse;
            }
            await using var transaction = await context.Database.BeginTransactionAsync();
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();
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



    public async Task<Response<Sale?>> UpdateAsync(UpdateSalesRequest request)
    {
        try
        {
       

            var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

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
    
            context.Sales.Update(sale);
            await context.SaveChangesAsync();
            return new Response<Sale?>(sale, message: "sale updated successfully");
            

        }
        catch
        {
            return new Response<Sale?>(null, 500, "It was not possible to update this sale");
        }
    }

    public async Task<Response<Sale?>> DeleteAsync(DeleteSalesRequest request)
    {
        try
        {
            var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (sale is null)
            {
                return new Response<Sale?>(null, 404, "sale not found");
            }

            context.Sales.Remove(sale);
            await context.SaveChangesAsync();
            return new Response<Sale?>(sale, message: "sale removed successfully");

        }
        catch
        {
            return new Response<Sale?>(null, 500, "It was not possible to delete this sale");
        }
    }

    public async Task<Response<Sale?>> GetByIdAsync(GetSalesByIdRequest request)
    {
        try
        {
            var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

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

    public async Task<PagedResponse<List<SaleList>?>> GetByPeriodAsync(GetAllSalesRequest request)
    {
        try
        {
            var query = context
                .Sales
                .AsNoTracking()
                .Include(s => s.Customer)
                .Include(s => s.Warehouse)
                .Include(s => s.Biller)
                .Where(x => x.UserId == request.UserId)
                .Select(g => new
                {
                    Id = g.Id,
                    SaleDate = g.SaleDate,
                    ReferenceNumber = g.ReferenceNumber,
                    CustomerName = g.Customer.Name,
                    WarehouseName = g.Warehouse.WarehouseName,
                    PaymentStatus = g.PaymentStatus,
                    SaleStatus = g.SaleStatus,
                    BillerName = g.Biller.Name,
                    TotalQuantitySold = g.TotalQuantitySold,
                    Discount = g.Discount,
                    TotalAmount = g.TotalAmount
                    
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
                PaymentStatus = s.PaymentStatus,
                SaleStatus = s.SaleStatus,
                BillerName = s.BillerName,
                TotalQuantitySold = s.TotalQuantitySold,
                Discount = s.Discount,
                TotalAmount = s.TotalAmount
                
            }).ToList();

            return new PagedResponse<List<SaleList>?>(
                result,
                count,
                request.PageNumber,
                request.PageSize);
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
            return new PagedResponse<List<BestSeller>?>(null, 500,
                "Not possible to determine the start or end date");
        }
        
        
        try
        {
           
            var query = context
                .Sales
                .AsNoTracking()
                .Include(x => x.Biller)
                .Where(x => 
                    x.SaleDate >= request.StartDate && 
                    x.SaleDate <= request.EndDate &&
                    x.UserId == request.UserId)
                .GroupBy(x => new { x.BillerId, x.Biller.Name })
                .Select(g => new
                {
                    BillerId = g.Key.BillerId,
                    BillerName = g.Key.Name,
                    TotalQuantitySold = g.Count(),
                    TotalAmount = g.Sum(x => x.TotalAmount),
                    
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
                TotalQuantitySold = s.TotalQuantitySold,
                TotalAmount = s.TotalAmount,
            }).ToList();

            return new PagedResponse<List<BestSeller>?>(
                result,
                count,
                request.PageNumber,
                request.PageSize);
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

            var query = context
                .SaleProducts
                .AsNoTracking()
                .Include(x => x.Sale)
                .Include(x => x.Product)
                .Where(x => 
                    x.Sale.SaleDate >= request.StartDate && 
                    x.Sale.SaleDate <= request.EndDate &&
                    x.Sale.UserId == request.UserId)
                .GroupBy(x => new { x.ProductId, x.Product.Title, x.Product.ProductCode  })
                .Select(g => new
                {
                    Id = g.Key.ProductId,
                    ProductCode = g.Key.ProductCode,
                    ProductName = g.Key.Title,
                    TotalQuantitySoldPerProduct = g.Count(),
                    
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

    public async Task<Response<decimal?>> GetTotalAmountSalesRequestAsync(GetTotalSalesAmountRequest request)
    {
        try
        {
            var totalSalesAmount = await context.Sales.SumAsync(x => x.TotalAmount);
            return new Response<decimal?>(totalSalesAmount, 200, "Total sales amount retrieved successfully");
        }
        catch 
        {
            return new Response<decimal?>(null, 500, "It was not possible to consult the total sale");
        }
        
    }
    
    
    public async  Task<Response<List<SalePerProduct>?>> GetSalesBySalesIdAsync(GetSalesBySaleIdRequest request)
    {
        
        try
        {
            var query = context
                .SaleProducts
                .AsNoTracking()
                .Include(x => x.Sale)
                .Include(x => x.Product)
                .Where(x => x.Sale.UserId == request.UserId && x.SaleId == request.SalesId)
                .GroupBy(x => new
                {
                    x.ProductId, x.Product.Title, x.ReferenceNumber, x.TotalQuantitySoldPerProduct, x.Product.Unit.ShortName,
                    
                })
                .Select(g => new
                {
                    Id = g.Key.ProductId,
                    ReferenceNumber = g.Key.ReferenceNumber,
                    ProductName = g.Key.Title,
                    TotalQuantitySoldPerProduct = g.Key.TotalQuantitySoldPerProduct,
                    UnitShortName = g.Key.ShortName
                    
                });

            var sale = await query.ToListAsync();

            var result = sale.Select(s => new SalePerProduct
            {
                ProductId = s.Id,
                UnitShortName = s.UnitShortName,
                ReferenceNumber = s.ReferenceNumber,
                TotalPricePerProduct = s.TotalQuantitySoldPerProduct,
                TotalQuantitySoldPerProduct = s.TotalQuantitySoldPerProduct,
                
            }).ToList();

            return new Response<List<SalePerProduct>?>(result, 200, "Items retrived Successfully");
        }
        catch
        {
            return new Response<List<SalePerProduct>?>(null, 500, "It was not possible to consult all sale");
        }
        
    }
    
    
    
    
    public async Task<Response<Sale?>> GetSalesBySellerAsync(GetSalesBySeller request)
    {
        try
        {
            var sale = await context.Sales.FirstOrDefaultAsync(x => x.BillerId == request.BillerId && x.UserId == request.UserId);

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
