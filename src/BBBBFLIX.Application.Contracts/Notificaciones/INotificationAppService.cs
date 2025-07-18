using BBBBFLIX.Notificaciones.Strategies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBBBFLIX.Notifications
{
    public interface INotificationAppService
    {
        Task CreateSendNotificationsAsync(int userId, string title, string message, NotificationType type);
        List<NotificationDto> ShowNotifications(int userId);
    }
}