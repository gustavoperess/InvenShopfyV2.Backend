using InvenShopfy.API.Data;
using InvenShopfy.API.EndPoints.Tradings.Purchase.Add;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Models.Tradings.Purchase;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.Tradings.Purchase.Add;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Tradings.Purchase;

public class PurchaseHandler(AppDbContext context) : IPurchaseHandler
{
    public async Task<Response<AddPurchase?>> CreateAsync(CreatePurchaseRequest request)
    {
        try
        {
            var purchase = new AddPurchase
            {
                UserId = request.UserId,
                WarehouseId = request.WarehouseId,
                SupplierId = request.SupplierId,
                ProductId = request.ProductId,
                PurchaseStatus = request.PurchaseStatus,
                ShippingCost = request.ShippingCost,
                PurchaseNote = request.PurchaseNote,
            };
            await context.Purchases.AddAsync(purchase);
            await context.SaveChangesAsync();

            return new Response<AddPurchase?>(purchase, 201, "Purchase created successfully");

        }
        catch
        {
            return new Response<AddPurchase?>(null, 500, "It was not possible to create a new purchase");
        }
    }

    public async Task<Response<AddPurchase?>> UpdateAsync(UpdatePurchaseRequest request)
    {
        try
        {
            var purchase = await context.Purchases.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (purchase is null)
            {
                return new Response<AddPurchase?>(null, 404, "Purchase not found");
            }
            
            purchase.WarehouseId = request.WarehouseId;
            purchase.SupplierId = request.SupplierId;
            purchase.ProductId = request.ProductId;
            purchase.PurchaseStatus = request.PurchaseStatus;
            purchase.ShippingCost = request.ShippingCost;
            purchase.PurchaseNote = request.PurchaseNote;
            context.Purchases.Update(purchase);
            await context.SaveChangesAsync();
            return new Response<AddPurchase?>(purchase, message: "Purchase updated successfully");
            

        }
        catch
        {
            return new Response<AddPurchase?>(null, 500, "It was not possible to update this Purchase");
        }
    }

    public async Task<Response<AddPurchase?>> DeleteAsync(DeletePurchaseRequest request)
    {
        try
        {
            var purchase = await context.Purchases.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (purchase is null)
            {
                return new Response<AddPurchase?>(null, 404, "purchase not found");
            }

            context.Purchases.Remove(purchase);
            await context.SaveChangesAsync();
            return new Response<AddPurchase?>(purchase, message: "purchase removed successfully");

        }
        catch
        {
            return new Response<AddPurchase?>(null, 500, "It was not possible to delete this purchase");
        }
    }

    public async Task<Response<AddPurchase?>> GetByIdAsync(GetPurchaseByIdRequest request)
    {
        try
        {
            var purchase = await context.Purchases.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (purchase is null)
            {
                return new Response<AddPurchase?>(null, 404, "purchase not found");
            }

            return new Response<AddPurchase?>(purchase);

        }
        catch
        {
            return new Response<AddPurchase?>(null, 500, "It was not possible to find this purchase");
        }
    }

    public async Task<PagedResponse<List<AddPurchase>?>> GetByPeriodAsync(GetAllPurchasesRequest request)
    {
        try
        {
            var query = context
                .Purchases
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Date);

            var purchase = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<AddPurchase>?>(
                purchase,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<AddPurchase>?>(null, 500, "It was not possible to consult all purchases");
        }
    }
}