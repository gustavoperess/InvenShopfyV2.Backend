using System.Text.Json;
using InvenShopfy.API.Data;
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
            var sale = new Sale
            {
                CustomerId = request.CustomerId,
                WarehouseId = request.WarehouseId,
                BillerId = request.BillerId,
                ShippingCost = request.ShippingCost,
                Document = request.Document,
                StaffNote = request.StaffNote,
                SaleNote = request.SaleNote,
                PaymentStatus = request.PaymentStatus,
                SaleStatus = request.SaleStatus,
                UserId = request.UserId,
            };
            // Console.WriteLine("Received CreateSalesRequest:");
            // Console.WriteLine($"CustomerId: {request.CustomerId}");
            // Console.WriteLine($"WarehouseId: {request.WarehouseId}");
            // Console.WriteLine($"BillerId: {request.BillerId}");
            // Console.WriteLine($"ShippingCost: {request.ShippingCost}");
            // Console.WriteLine($"Document: {request.Document}");
            // Console.WriteLine($"StaffNote: {request.StaffNote}");
            // Console.WriteLine($"SaleNote: {request.SaleNote}");
            // Console.WriteLine($"PaymentStatus: {request.PaymentStatus}");
            // Console.WriteLine($"SaleStatus: {request.SaleStatus}");
            // Console.WriteLine($"UserId: {request.UserId}");
            

            foreach (var product in request.ProductIdPlusQuantity)
            {
                Console.WriteLine(product.Key);
                Console.WriteLine(product.Value);
                
            }
            
            foreach (var item in request.ProductIdPlusQuantity)
            {
             
                var product = await context.Products.FirstOrDefaultAsync(p => p.Id == item.Key);
                if (product == null)
                {
                    return new Response<Sale?>(null, 400, $"Product with Id {product} not found");
                }
                
                var saleProduct = sale.CreateSaleProduct(product.Id, product.Price, item.Value);
                sale.SaleProducts.Add(saleProduct);
            }
            
            sale.TotalQuantitySold = sale.SaleProducts.Sum(x => x.TotalQuantitySoldPerProduct);
            sale.TotalAmount = sale.SaleProducts.Sum(sp => (sp.TotalPricePerProduct * sp.TotalQuantitySoldPerProduct)) + request.ShippingCost;
            
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

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
                .OrderBy(x => x.Date);

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
