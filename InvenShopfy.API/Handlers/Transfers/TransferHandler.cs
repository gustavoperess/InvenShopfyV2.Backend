using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Transfer;
using InvenShopfy.Core.Models.Transfer;
using InvenShopfy.Core.Models.Warehouse;
using InvenShopfy.Core.Requests.Transfers;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Transfers;

public class TransferHandler(AppDbContext context) : ITransferHandler
{
    public async Task<Response<Transfer?>> CreateTransferAsyncAsync(CreateTransferRequest request)
    {
        try
        {
            var transfer = new Transfer
            {
                UserId = request.UserId,
                Quantity = request.Quantity,
                AuthorizedBy = request.AuthorizedBy,
                Reason = request.Reason,
                TransferDate = request.TransferDate,
                FromWarehouseId = request.FromWarehouseId,
                ToWarehouseId = request.ToWarehouseId,
                TransferStatus = request.TransferStatus,
                TransferNote = request.TransferNote,
                ProductId = request.ProductId
            };
            await using var transaction = await context.Database.BeginTransactionAsync();

            var fromWarehouse = await context.WarehousesProducts.
                FirstOrDefaultAsync(x => x.WarehouseId == request.FromWarehouseId && x.ProductId == request.ProductId);
            
            
            var toWarehouse = await context.WarehousesProducts
                .FirstOrDefaultAsync(x => x.WarehouseId == request.ToWarehouseId && x.ProductId == request.ProductId);

            if (fromWarehouse == null)
            {
                return new Response<Transfer?>(null, 400, "One or both of the specified warehouses were not found.");
            }

            if (toWarehouse == null)
            {
                var newWarehouseProduct = new WarehouseProduct
                {
                    WarehouseId = request.FromWarehouseId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                await context.WarehousesProducts.AddAsync(newWarehouseProduct);
            }
            
            fromWarehouse.Quantity -= request.Quantity;
            toWarehouse.Quantity += request.Quantity;
            
            await context.Transfers.AddAsync(transfer);
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
            return new Response<Transfer?>(transfer, 201, "Transfer created successfully");
        }
        catch
        {
            return new Response<Transfer?>(null, 500, "It was not possible to create a new Brand");
        }
    }
}