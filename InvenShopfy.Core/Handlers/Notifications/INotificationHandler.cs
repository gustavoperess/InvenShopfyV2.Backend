using InvenShopfy.Core.Requests.Notifications;
using InvenShopfy.Core.Responses;
using InvenShopfy.Core.Models.Notifications;

namespace InvenShopfy.Core.Handlers.Notifications;

public interface INotificationHandler 
{
    Task<Response<Notification?>> CreateNotificationRequest(CreateNotificationsRequest request);
}