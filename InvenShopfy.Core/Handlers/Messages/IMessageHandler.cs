using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Messages;

public interface IMessageHandler
{
    Task<Response<Message?>> CreateMessageAsync(CreateMessageRequest request);
    
    Task<PagedResponse<List<MessageDto>?>> GetSentMessagesAsync(GetAllMessagesRequest request);
    Task<Response<int?>> CountSentMessageAsync(GetAllMessagesRequest request);
    
    Task<Response<int?>> CountInboxMessagesAsync(GetAllMessagesRequest request);
    
    Task<Response<int?>> CountImportantMessagesAsync(GetAllMessagesRequest request);
    
    Task<Response<int?>> CountTrashtMessagesAsync(GetAllMessagesRequest request);
    
    Task<PagedResponse<List<MessageDto>?>> GetImportantMessageAsync(GetAllMessagesRequest request);
    
    Task<PagedResponse<List<MessageDto>?>> GetTrashMessageAsync(GetAllMessagesRequest request);
    
    Task<Response<Message?>> MoveMessageToImportantAsycn(MoveMessageRequest request);
    
    Task<Response<Message?>> MoveMessageToTrashAsycn(MoveMessageRequest request);
    
    Task<PagedResponse<List<MessageDto>?>> GetInboxMessageAsync(GetAllMessagesRequest request);
    

}