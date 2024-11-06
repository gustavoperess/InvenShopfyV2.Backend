using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Transfer;
using InvenShopfy.Core.Models.Transfer;
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
                ProductName = request.ProductName,
                Quantity = request.Quantity,
                AuthorizedBy = request.AuthorizedBy,
                Reason = request.Reason,
                TransferDate = request.TransferDate,
                FromWarehouseId = request.FromWarehouseId,
                ToWarehouseId = request.ToWarehouseId,
                TransferStatus = request.TransferStatus,
                TransferNote = request.TransferNote
            };
            await using var transaction = await context.Database.BeginTransactionAsync();

            var fromWarehouse = await context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.FromWarehouseId);
            var toWarehouse = await context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.ToWarehouseId);

            if (fromWarehouse == null || toWarehouse == null)
            {
                return new Response<Transfer?>(null, 400, "One or both of the specified warehouses were not found.");
            }


            fromWarehouse.QuantityOfItems -= request.Quantity;
            toWarehouse.QuantityOfItems += request.Quantity;
            
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