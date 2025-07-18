using BBBBFLIX.EntityFrameworkCore;
using BBBBFLIX.Notificaciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BBBBFLIX.Notifications
{
    public class NotificacionRepository : EfCoreRepository<BBBBFLIXDbContext, Notification, int>, INotificationRepository
    {
        public NotificacionRepository(IDbContextProvider<BBBBFLIXDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<List<Notification>> GetUnreadNotificationsAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }
        /*
       public async Task<List<Notification>> GetUnreadNotificationsAsync(int usuarioId)
       {
           return await DbSet
               .Where(n => n.UsuarioId == usuarioId && !n.Leido)
               .ToListAsync();
       }
*/
    }
}
