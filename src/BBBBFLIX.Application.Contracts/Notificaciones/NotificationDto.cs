using BBBBFLIX.Notificaciones.Strategies;
using System;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Notifications
{
    public class NotificationDto : EntityDto<int>
    {
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public NotificationType Type { get; set; }
        public required string Title { get; set; }
        public required string Message { get; set; }
        public bool IsRead { get; set; }
    }
}