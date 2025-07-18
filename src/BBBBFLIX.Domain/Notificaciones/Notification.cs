using BBBBFLIX.Notificaciones.Strategies;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace BBBBFLIX.Notifications
{
    public class Notification : FullAuditedAggregateRoot<int>
    {
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public NotificationType Type { get; set; }
        public required string Title { get; set; }
        public required string Message { get; set; }
        public bool IsRead { get; set; }
    }
}