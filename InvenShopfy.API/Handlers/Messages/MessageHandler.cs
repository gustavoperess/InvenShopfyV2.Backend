using System.Globalization;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.Handlers.Messages;

public class MessageHandler(AppDbContext context) : IMessageHandler
{
    public async Task<Response<Message?>> CreateMessageAsync(CreateMessageRequest request)
    {
        try
        {
            var product = new Message
            {
                UserId = request.UserId,
                Title = request.Title,
                Subtitle = request.Subtitle,
                ToUserId = request.ToUserId,
                Description = request.Description,
                IsImportant = request.IsImportant,
                IsSent = true,
        
            };
            
            await context.Messages.AddAsync(product);
            await context.SaveChangesAsync();
            
            return new Response<Message?>(product, 201, "Message created successfully");
        }
        catch
        {
            
            return new Response<Message?>(null, 500, "It was not possible to create a new Message");
        }
    }
}