using InvenShopfy.Core.Requests.Transfers;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Transfer;

public interface ITransferHandler
{
    Task<Response<Models.Transfer.Transfer?>> CreateTransferAsyncAsync(CreateTransferRequest request);
    
    
    Task<PagedResponse<List<Models.Transfer.Dto.TransferDto>?>> GetAllTransfersAsync(GetAllTransfersRequest request);


}