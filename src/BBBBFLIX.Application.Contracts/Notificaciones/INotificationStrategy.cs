using BBBBFLIX.Notificaciones.Strategies;
using BBBBFLIX.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBBFLIX.Notificaciones
{
    public interface INotificationStrategy
    {
        Task SendNotificationAsync(NotificationDto notification);
        bool CanSend(NotificationType type);
    }
}
