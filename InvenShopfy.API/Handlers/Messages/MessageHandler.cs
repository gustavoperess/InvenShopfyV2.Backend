using InvenShopfy.API.Data;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Messages;

public class MessageHandler: IMessageHandler
{
    private readonly AppDbContext _context;
    private readonly  UserManager<CustomUserRequest> _userManager;
    public MessageHandler(AppDbContext context,  UserManager<CustomUserRequest> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<Response<Message?>> CreateMessageAsync(CreateMessageRequest request)
    {
        try
        {
            var product = new Message
            {
                UserId = request.UserId,
                Title = request.Title,
                Subject = request.Subject,
                ToUserId = request.ToUserId,
                MessageBody = request.MessageBody,
                IsSent = true,
                IsImportant = false,
                IsDeleted = false,
                IsReceived = false
        
            };
            
            await _context.Messages.AddAsync(product);
            await _context.SaveChangesAsync();
            
            return new Response<Message?>(product, 201, "Message created successfully");
        }
        catch
        {
            
            return new Response<Message?>(null, 500, "It was not possible to create a new Message");
        }
    }
    
    
    public async Task<Response<Message?>> MoveMessageToImportantAsycn(MoveMessageToImportantRequest request)
    {
        try
        {
            var message = _context.Messages.FirstOrDefault(x => x.Id == request.Id);
            if (message == null)
            {
                return new Response<Message?>(null, 500, "Message Not found");
            }

            if (message.IsImportant)
            {
                message.IsImportant = false;
            } 
            else if (!message.IsImportant)
            {
                message.IsImportant = true;
            }
            
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
            
            return new Response<Message?>(message, 201, "Message created successfully");
        }
        catch
        {
            
            return new Response<Message?>(null, 500, "It was not possible to create a new Message");
        }
    }
    
    public async Task<PagedResponse<List<MessageDto>?>> GetSentMessagesAsync(GetAllMessagesRequest request)
    {
        try
        {
            var query =  _context.Messages
                .AsNoTracking()
                .Join(_userManager.Users,
                    ul => ul.ToUserId,
                    ur => ur.Id,
                    (message, userInfo) => new { message, userInfo })
                .Where(x => x.message.UserId == request.UserId)
                .Select(g => new
                {
                    g.message.Id,
                    g.userInfo.Name,
                    g.userInfo.ProfilePicture,
                    g.message.Time,
                    g.message.Title,
                    g.message.Subject,
                    g.message.MessageBody
                }).OrderByDescending(x => x.Time);
            
            var message = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            
            var result = message.Select(s => new MessageDto
            {
                Id = s.Id,
                Title = s.Title,
                Subject = s.Subject,
                ToUser = s.Name,
                ProfilePicture = s.ProfilePicture,
                MessageBody = s.MessageBody,
                Time = s.Time,
                
            }).ToList();
            
            return new PagedResponse<List<MessageDto>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            
            return new PagedResponse<List<MessageDto>?>(null, 500, "It was not possible to received the sent messages");
        }
    }
    
    
    public async Task<Response<int?>> CountSentMessageAsync(GetAllMessagesRequest request)
    {
        try
        {
            var query = _context.Messages.AsNoTracking()
                .Join(_userManager.Users,
                    ul => ul.ToUserId,
                    ur => ur.Id,
                    (message, user) => new { message, user })
                .Where(x => x.message.UserId == request.UserId);

            var count = await query.CountAsync();

            return new Response<int?>(count, 200, "Sent total Amount retrived sucessfully");
        }
        catch
        {
            
            return new PagedResponse<int?>(null, 500, "It was not possible to retrive total sent amount");
        }
    }
    
    public async Task<Response<int?>> CountInboxMessagesAsync(GetAllMessagesRequest request)
    {
        try
        {
            var query = _context.Messages.AsNoTracking()
                .Join(_userManager.Users,
                    ul => ul.ToUserId,
                    ur => ur.Id,
                    (message, user) => new { message, user })
                .Where(x => x.user.UserName == request.UserId);

            var count = await query.CountAsync();

            return new Response<int?>(count, 200, "Inbox total Amount retrived sucessfully");
        }
        catch
        {
            
            return new PagedResponse<int?>(null, 500, "It was not possible to retrive total Inbox amount");
        }
    }
    
    public async Task<PagedResponse<List<MessageDto>?>> GetInboxMessageAsync(GetAllMessagesRequest request)
    {
        try
        {
            var query =  _context.Messages
                .AsNoTracking()
                .Join(_userManager.Users,
                    ul => ul.UserId,
                    ur => ur.UserName,
                    (message, receiverInfo) => new { message, receiverInfo })
                .Join(_userManager.Users,
                    ul => ul.message.ToUserId,
                    ur => ur.Id,
                    (messageWithSender, senderInfo) => new
                    {
                        messageWithSender.message.Id,
                        SenderUserName = senderInfo.UserName,
                        messageWithSender.receiverInfo.ProfilePicture,
                        ReceiverUserName = messageWithSender.receiverInfo.Name,
                        messageWithSender.message.MessageBody,
                        messageWithSender.message.Time,
                        messageWithSender.message.Subject,
                        messageWithSender.message.Title,
                        messageWithSender.message.IsDeleted,
                        messageWithSender.message.IsImportant,
                    })
                .Where(x => x.SenderUserName == request.UserId && !x.IsDeleted && !x.IsImportant)
                .Select(g => new
                {
                    g.Id,
                    g.ReceiverUserName,
                    g.ProfilePicture,
                    g.Time,
                    g.Title,
                    g.Subject,
                    g.MessageBody
                });
            
            var message = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        
            var count = await query.CountAsync();
            
            var result = message.Select(s => new MessageDto
            {
                Id = s.Id,
                Title = s.Title,
                Subject = s.Subject,
                ToUser = s.ReceiverUserName,
                ProfilePicture = s.ProfilePicture,
                MessageBody = s.MessageBody,
                Time = s.Time,
                
            }).ToList();
            
            return new PagedResponse<List<MessageDto>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            
            return new PagedResponse<List<MessageDto>?>(null, 500, "It was not possible to received the sent messages");
        }
    }
    
    public async Task<PagedResponse<List<MessageDto>?>> GetImportantMessageAsync(GetAllMessagesRequest request)
    {
        try
        {
            var query =  _context.Messages
                .AsNoTracking()
                .Join(_userManager.Users,
                    ul => ul.UserId,
                    ur => ur.UserName,
                    (message, receiverInfo) => new { message, receiverInfo })
                .Join(_userManager.Users,
                    ul => ul.message.ToUserId,
                    ur => ur.Id,
                    (messageWithSender, senderInfo) => new
                    {
                        messageWithSender.message.Id,
                        SenderUserName = senderInfo.UserName,
                        messageWithSender.receiverInfo.ProfilePicture,
                        ReceiverUserName = messageWithSender.receiverInfo.Name,
                        messageWithSender.message.MessageBody,
                        messageWithSender.message.Time,
                        messageWithSender.message.Subject,
                        messageWithSender.message.Title,
                        messageWithSender.message.IsDeleted,
                        messageWithSender.message.IsImportant,
                    })
                .Where(x => x.SenderUserName == request.UserId && !x.IsDeleted && x.IsImportant)
                .Select(g => new
                {
                    g.Id,
                    g.ReceiverUserName,
                    g.ProfilePicture,
                    g.Time,
                    g.Title,
                    g.Subject,
                    g.MessageBody
                });
            
            var message = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        
            var count = await query.CountAsync();
            
            var result = message.Select(s => new MessageDto
            {
                Id = s.Id,
                Title = s.Title,
                Subject = s.Subject,
                ToUser = s.ReceiverUserName,
                ProfilePicture = s.ProfilePicture,
                MessageBody = s.MessageBody,
                Time = s.Time,
                
            }).ToList();
            
            return new PagedResponse<List<MessageDto>?>(result, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            
            return new PagedResponse<List<MessageDto>?>(null, 500, "It was not possible to received the sent messages");
        }
    }
}