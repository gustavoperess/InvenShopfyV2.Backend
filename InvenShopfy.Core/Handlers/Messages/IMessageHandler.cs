using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Messages;

public interface IMessageHandler
{
    Task<Response<Message?>> CreateMessageAsync(CreateMessageRequest request);
    
    Task<PagedResponse<List<MessageDto>?>> GetSentMessagesAsync(GetAllMessagesRequest request);
    //
    // Task<PagedResponse<List<Message>?>> GetImportantMessageAsync(GetAllMessagesRequest request);
    //
    // Task<PagedResponse<List<Message>?>> GetInboxMessageAsync(GetAllMessagesRequest request);

}