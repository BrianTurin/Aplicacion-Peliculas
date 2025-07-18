using BBBBFLIX.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BBBBFLIX.Notificaciones
{
    public interface INotificationRepository : IRepository<Notification, int>
    {
        Task<List<Notification>> GetUnreadNotificationsAsync(int userId);
    }
}
