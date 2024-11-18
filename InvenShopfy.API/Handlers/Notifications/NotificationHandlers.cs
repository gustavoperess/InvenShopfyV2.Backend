using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Notifications;
using InvenShopfy.Core.Models.Notifications;
using InvenShopfy.Core.Requests.Notifications;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Notifications;

public class NotificationHandlers(AppDbContext context) : INotificationHandler
{
    public async Task<Response<Notification?>> CreateNotificationRequest(CreateNotificationsRequest request)
    {
        try
        {
            var notificationsCount = await context.Notifications.CountAsync();
            if (notificationsCount >= 5)
            {
                var oldestNotification = await context.Notifications.OrderBy(n => n.CreateAt).FirstOrDefaultAsync();
                if (oldestNotification != null)
                {
                    context.Notifications.Remove(oldestNotification);
                }
            }
            var notification = new Notification
            {
                
                Title = request.Title,
                Urgency = request.Urgency,
                From = request.From,
                Image = request.Image,
                CreateAt = DateOnly.FromDateTime(DateTime.Now)
        
            };
            if (request.Image == null)
            {
                notification.Image = "https://res.cloudinary.com/dououppib/image/upload/v1731940894/InvenShopfy/pzqvha2mmvtt0uhsl8um.png";
            }
            
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
            return new Response<Notification?>(notification, 201, "Product created successfully");
        }
        catch
        {
            
            return new Response<Notification?>(null, 500, "It was not possible to create a new Product");
        }
    }
}