using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Notifications;
using InvenShopfy.Core.Handlers.Transfer;
  using InvenShopfy.Core.Models.Transfer;
  using InvenShopfy.Core.Models.Transfer.Dto;
  using InvenShopfy.Core.Models.Warehouse;
  using InvenShopfy.Core.Requests.Notifications;
  using InvenShopfy.Core.Requests.Transfers;
  using InvenShopfy.Core.Responses;
  using Microsoft.EntityFrameworkCore;
  
  namespace InvenShopfy.API.Handlers.Transfers;
  
  public class TransferHandler : ITransferHandler
  {
      private readonly AppDbContext _context;
      private readonly INotificationHandler _notificationHandler; 

      public TransferHandler(AppDbContext context,INotificationHandler notificationHandler)
      {
          _context = context;
          _notificationHandler = notificationHandler;
      }

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
              
              
              await using var transaction = await _context.Database.BeginTransactionAsync();
  
              var fromWarehouse = await _context.WarehousesProducts.
                  FirstOrDefaultAsync(x => x.WarehouseId == request.FromWarehouseId && x.ProductId == request.ProductId);
              
              var toWarehouse = await _context.WarehousesProducts
                  .FirstOrDefaultAsync(x => x.WarehouseId == request.ToWarehouseId && x.ProductId == request.ProductId);
  
              if (fromWarehouse == null)
              {
                  return new Response<Transfer?>(null, 400, "One or both of the specified warehouses were not found.");
              }
              
              if (toWarehouse == null)
              {
                  var newWarehouseProduct = new WarehouseProduct
                  {
                      WarehouseId = request.ToWarehouseId,
                      ProductId = request.ProductId,
                      Quantity = request.Quantity
                  };
                   _context.WarehousesProducts.Add(newWarehouseProduct);
                   fromWarehouse.Quantity -= request.Quantity;
              }
              else
              {
                  fromWarehouse.Quantity -= request.Quantity;
                  toWarehouse.Quantity += request.Quantity;
              }
           
      
              await _context.Transfers.AddAsync(transfer);
              await _context.SaveChangesAsync();
              
              var notificationRequest = new CreateNotificationsRequest
              {
                  Title =  $"New Product Transfer : {request.Quantity} created",
                  Urgency = true,
                  From = "System-Transfer", 
                  Image = null, 
                  UserId = request.UserId,
              };
              await _notificationHandler.CreateNotificationAsync(notificationRequest);
  
              await transaction.CommitAsync();
              return new Response<Transfer?>(transfer, 201, "Transfer created successfully");
          }
          catch
          {
              return new Response<Transfer?>(null, 500, "It was not possible to create a new Transfer");
          }
      }
      
      public async Task<PagedResponse<List<TransferDto>?>> GetAllTransfersAsync(GetAllTransfersRequest request)
    {
        try
        {
            var query = _context
                .Transfers
                .AsNoTracking()
                .Include(x => x.FromWarehouse)
                .Include(x => x.ToWarehouse)
                .Include(x => x.Product)
                .Select(g => new
                {
                    g.Id,
                    FromWarehouse = g.FromWarehouse.WarehouseName, 
                    ToWarehouse =g.ToWarehouse.WarehouseName,
                    g.ReferenceNumber,
                    g.Quantity,
                    g.TransferDate,
                    g.AuthorizedBy,
                    g.Reason,
                    g.TransferStatus,
                    g.TransferNote,
                    ProductName = g.Product.Title
                    
                })
                .OrderBy(x => x.TransferDate);
            
            var count = await query.CountAsync();
            
            var transfers = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = transfers.Select(s => new TransferDto
            {
                Id = s.Id,
                FromWarehouse = s.FromWarehouse,
                ToWarehouse = s.ToWarehouse,
                ReferenceNumber = s.ReferenceNumber,
                Quantity = s.Quantity,
                TransferNote = s.TransferNote,
                AuthorizedBy = s.AuthorizedBy,
                TransferStatus = s.TransferStatus,
                Reason = s.Reason,
                ProductName = s.ProductName,
                TransferDate = s.TransferDate
                
            }).ToList();
            
            return new PagedResponse<List<TransferDto>?>(
                result,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<TransferDto>?>(null, 500, "It was not possible to consult all transfer");
        }
    }
    
    
    
}