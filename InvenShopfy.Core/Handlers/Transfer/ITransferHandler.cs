using InvenShopfy.Core.Requests.Transfers;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Transfer;

public interface ITransferHandler
{
    Task<Response<Models.Transfer.Transfer?>> CreateTransferAsyncAsync(CreateTransferRequest request);

}