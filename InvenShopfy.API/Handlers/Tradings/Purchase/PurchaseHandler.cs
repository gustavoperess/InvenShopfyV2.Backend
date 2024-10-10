using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Models.Tradings.Purchase;
using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Tradings.Purchase;

public class PurchaseHandler(AppDbContext context) : IPurchaseHandler
{
    
    public async Task<Response<AddPurchase?>> CreateAsync(CreatePurchaseRequest request)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(); // this ensures that all operations are successed. if one fail the whole thing fails
        try
        {
            var purchase = new AddPurchase
            {
                UserId = request.UserId,
                WarehouseId = request.WarehouseId,
                SupplierId = request.SupplierId,
                PurchaseStatus = request.PurchaseStatus,
                ShippingCost = request.ShippingCost,
                PurchaseNote = request.PurchaseNote,
                PurchaseDate = request.PurchaseDate,
                TotalAmountBought = request.TotalAmountBought,
            };
            
            var productIds = request.ProductIdPlusQuantity.Keys;
            var availablePurchaseProducts =
                await context.Products.Where(sp => productIds.Contains(sp.Id)).ToListAsync();
            // var availablePurchaseProducts = await context.PurchaseProducts
            //     .Include(sp => sp.Product) 
            //     .Where(sp => productIds.Contains(sp.ProductId))
            //     .ToListAsync();

            var pruchaseRespose = purchase.AddPurchaseToPurchase(request.ProductIdPlusQuantity, availablePurchaseProducts);
            if (!pruchaseRespose.IsSuccess)
            {
                return pruchaseRespose;
            }
            
            // foreach (var pur in availablePurchaseProducts)
            // {
            //     context.Products.Update(pur.Product);
            // }
            //
            await context.Purchases.AddAsync(purchase);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return new Response<AddPurchase?>(purchase, 201, "Purchase created successfully");

        }
        catch
        {
            await transaction.RollbackAsync(); // rollback if the transaction fails 
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

    public async Task<PagedResponse<List<PurchaseList>?>> GetByPeriodAsync(GetAllPurchasesRequest request)
    {
        try
        {
            var query = context
                .Purchases
                .AsNoTracking()
                .Include(x => x.Warehouse)
                .Include(x => x.Supplier)
                .Where(x => x.UserId == request.UserId)
                .Select(g => new
                {
                  g.Id,  
                  g.PurchaseDate,  
                  SupplierName = g.Supplier.Name,  
                  g.Warehouse.WarehouseName,  
                  g.PurchaseStatus,  
                  g.ShippingCost,  
                  g.TotalAmountBought,  
                  g.ReferenceNumber,
                  g.TotalNumberOfProductsBought
                })
                .OrderBy(x => x.PurchaseDate);

            var purchase = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = purchase.Select(p => new PurchaseList
            {
                Id = p.Id,  
                PurchaseDate = p.PurchaseDate,  
                SupplierName = p.SupplierName,  
                WarehouseName = p.WarehouseName,  
                PurchaseStatus = p.PurchaseStatus,  
                ShippingCost = p.ShippingCost,  
                TotalAmountBought = p.TotalAmountBought,  
                ReferenceNumber = p.ReferenceNumber,
                TotalNumberOfProductsBought = p.TotalNumberOfProductsBought

            }).ToList();

            var count = await query.CountAsync();

            return new PagedResponse<List<PurchaseList>?>(
                result,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<PurchaseList>?>(null, 500, "It was not possible to consult all purchases");
        }
    }
}