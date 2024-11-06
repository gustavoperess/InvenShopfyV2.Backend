using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn.Dto;
using InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Tradings.Returns;

public class PurchaseReturnHandlers(AppDbContext context) : IPurchaseReturnHandler
{
    public async Task<Response<PurchaseReturn?>> CreatePurchaseReturnAsync(CreatePurchaseReturnRequest request)
    {
        try
        {
            var purchasereturn = new PurchaseReturn
            {
                UserId = request.UserId,
                ReturnDate = request.ReturnDate,
                SupplierName = request.SupplierName,
                TotalAmount = request.TotalAmount,
                WarehouseName = request.WarehouseName,
                RemarkStatus = request.Remark,
                ReturnNote = request.ReturnNote,
                ReferenceNumber = request.ReferenceNumber,
            };
            
            // remove item from purchaseReturn.
            var findPurchaseByReferenceNumber = await context.Purchases.FirstOrDefaultAsync(x => x.ReferenceNumber == request.ReferenceNumber);
            if (findPurchaseByReferenceNumber != null)
            {
                context.Purchases.Remove(findPurchaseByReferenceNumber);
            }
     
            
            await context.PurchaseReturns.AddAsync(purchasereturn);
            await context.SaveChangesAsync();
            return new Response<PurchaseReturn?>(purchasereturn, 201, "PurchaseReturn created successfully");
        }
        catch
        {
            return new Response<PurchaseReturn?>(null, 500, "It was not possible to create a new PurchaseReturn");

        }
    }
    
    
    public async Task<Response<List<PurchaseReturnByReturnNumber>?>> GetPurchasePartialByReferenceNumberAsync(GetPurchaseReturnByNumberRequest request)
    {
        try
        {
            var returns = await context.Purchases
                .AsNoTracking()
                .Where(x => EF.Functions.ILike(x.ReferenceNumber, $"%{request.ReferenceNumber}%") && x.UserId == request.UserId)
                .Select(g => new PurchaseReturnByReturnNumber
                {
                    Id = g.Id,
                    TotalAmount = g.TotalAmountBought,
                    WarehouseName = g.Warehouse.WarehouseName,
                    SupplierName= g.Supplier.Name,
                    ReferenceNumber = g.ReferenceNumber,
                }).OrderBy(x => x.SupplierName)
                .ToListAsync();
            
            if (returns.Count == 0)
            {
                return new PagedResponse<List<PurchaseReturnByReturnNumber>?>(new List<PurchaseReturnByReturnNumber>(), 200, "No returns found");
            }
            
            return new PagedResponse<List<PurchaseReturnByReturnNumber>?>(returns);
    
        }
        catch
        {
            return new PagedResponse<List<PurchaseReturnByReturnNumber>?>(null, 500, "It was not possible to consult all returns");
        }
    }

    public async Task<PagedResponse<List<PurchaseReturn>?>> GetAllPurchaseReturnAsync(GetAllPurchaseReturnsRequests request)
    {
        try
        {
            var query = context.PurchaseReturns
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.ReturnDate);
            
            var purchaseReturn = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<PurchaseReturn>?>(
                purchaseReturn,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<PurchaseReturn>?>(null, 500, "It was not possible to consult all PurchaseReturns");
        }
    }
    
    public async Task<Response<PurchaseReturn?>> DeletePurchaseReturnAsync(DeletePurchaseReturnRequest request)
    {
        try
        {
            var purchaseReturn = await context.PurchaseReturns.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (purchaseReturn is null)
            {
                return new Response<PurchaseReturn?>(null, 404, "purchaseReturn not found");
            }

            context.PurchaseReturns.Remove(purchaseReturn);
            await context.SaveChangesAsync();
            return new Response<PurchaseReturn?>(purchaseReturn, message: "purchaseReturn removed successfully");

        }
        catch 
        {
            return new Response<PurchaseReturn?>(null, 500, "It was not possible to delete this purchaseReturn");
        }
    }
}