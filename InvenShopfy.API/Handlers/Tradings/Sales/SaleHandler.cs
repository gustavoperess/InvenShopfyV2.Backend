using System.Text.Json;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Tradings.Sales;
// NEXT REDUCE THE PRODUCT STOCK FROM THE PRODUCTS AFTER SOLD
// ADD IMAGE TO THE PRODUCTS 
public class SaleHandler(AppDbContext context) : ISalesHandler 
{
    public async Task<Response<Sale?>> CreateAsync(CreateSalesRequest request)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var sale = new Sale
            {
                CustomerId = request.CustomerId,
                WarehouseId = request.WarehouseId,
                BillerId = request.BillerId,
                SaleDate = request.SaleDate,
                ShippingCost = request.ShippingCost,
                Document = request.Document,
                StaffNote = request.StaffNote,
                SaleNote = request.SaleNote,
                PaymentStatus = request.PaymentStatus,
                SaleStatus = request.SaleStatus,
                UserId = request.UserId,
                TotalAmount = request.TotalAmount,
                Discount = request.Discount
            };
            
            foreach (var item in request.ProductIdPlusQuantity)
            {
             
                var product = await context.Products.FirstOrDefaultAsync(p => p.Id == item.Key);
                if (product == null)
                {
                    return new Response<Sale?>(null, 400, $"Product with Id {item.Key} not found");
                }

                if (product.StockQuantity < item.Value)
                {
                    return new Response<Sale?>(null, 400, $"Insufficient quantity for product Id {item.Key}");
                }

                var pricePerProduct = product.Price * item.Value;
                var saleProduct = sale.CreateSaleProduct(product.Id, pricePerProduct, item.Value);
                sale.SaleProducts.Add(saleProduct);
                product.StockQuantity -= item.Value;
                context.Products.Update(product);
            }
            
            sale.TotalQuantitySold = sale.SaleProducts.Sum(x => x.TotalQuantitySoldPerProduct);
            
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();
            
            await transaction.CommitAsync();

            return new Response<Sale?>(sale, 201, "Sale created successfully");
        }
        catch
        {
            return new Response<Sale?>(null, 500, "It was not possible to create a new sale");
        }
    }


    public async Task<Response<Core.Models.Tradings.Sales.Sale?>> UpdateAsync(UpdateSalesRequest request)
    {
        try
        {
       

            var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (sale is null)
            {
                return new Response<Core.Models.Tradings.Sales.Sale?>(null, 404, "sale not found");
            }
            

            sale.CustomerId = request.CustomerId;
            sale.WarehouseId = request.WarehouseId;
            sale.BillerId = request.BillerId;
            // sale.ProductId = request.ProductId;
            sale.ShippingCost = request.ShippingCost;
            sale.Document = request.Document;
            sale.StaffNote = request.StafNote;
            sale.SaleNote = request.SaleNote;
    
            context.Sales.Update(sale);
            await context.SaveChangesAsync();
            return new Response<Core.Models.Tradings.Sales.Sale?>(sale, message: "sale updated successfully");
            

        }
        catch
        {
            return new Response<Core.Models.Tradings.Sales.Sale?>(null, 500, "It was not possible to update this sale");
        }
    }

    public async Task<Response<Core.Models.Tradings.Sales.Sale?>> DeleteAsync(DeleteSalesRequest request)
    {
        try
        {
            var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (sale is null)
            {
                return new Response<Core.Models.Tradings.Sales.Sale?>(null, 404, "sale not found");
            }

            context.Sales.Remove(sale);
            await context.SaveChangesAsync();
            return new Response<Core.Models.Tradings.Sales.Sale?>(sale, message: "sale removed successfully");

        }
        catch
        {
            return new Response<Core.Models.Tradings.Sales.Sale?>(null, 500, "It was not possible to delete this sale");
        }
    }

    public async Task<Response<Core.Models.Tradings.Sales.Sale?>> GetByIdAsync(GetSalesByIdRequest request)
    {
        try
        {
            var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (sale is null)
            {
                return new Response<Core.Models.Tradings.Sales.Sale?>(null, 404, "sale not found");
            }

            return new Response<Core.Models.Tradings.Sales.Sale?>(sale);

        }
        catch
        {
            return new Response<Core.Models.Tradings.Sales.Sale?>(null, 500, "It was not possible to find this sale");
        }
    }

    public async Task<PagedResponse<List<Core.Models.Tradings.Sales.Sale>?>> GetByPeriodAsync(GetAllSalesRequest request)
    {
        try
        {
            var query = context
                .Sales
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.SaleDate);

            var sale = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Core.Models.Tradings.Sales.Sale>?>(
                sale,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Core.Models.Tradings.Sales.Sale>?>(null, 500, "It was not possible to consult all sale");
        }
    }

    public async Task<Response<double?>> GetTotalAmountSalesRequestAsync(GetTotalSalesAmountRequest request)
    {
        try
        {
            var totalSalesAmount = await context.Sales.SumAsync(x => x.TotalAmount);
            return new Response<double?>(totalSalesAmount, 200, "Total sales amount retrieved successfully");
        }
        catch 
        {
            return new Response<double?>(null, 500, "It was not possible to consult the total sale");
        }
        
    }
}
