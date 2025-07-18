using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using BBBBFLIX.Notificaciones.Strategies;
using BBBBFLIX.Notificaciones;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace BBBBFLIX.Notifications
{
    public class NotificacionService : INotificationAppService, ITransientDependency
    { 
        private readonly INotificationRepository _notificacionRepository;
        private readonly IEnumerable<INotificationStrategy> _notificationStrategies;
        private readonly ILogger<NotificacionService> _logger;

        public NotificacionService(
            INotificationRepository notificacionRepository,
            IEnumerable<INotificationStrategy> notificationStrategies,
            ILogger<NotificacionService> logger)
        {
            _notificacionRepository = notificacionRepository;
            _notificationStrategies = notificationStrategies;
            _logger = logger;
        }

        public async Task CreateSendNotificationsAsync(int userId, string title, string message, NotificationType type)
        {
            _logger.LogInformation("Creating and sending notifications");
            // Pasar datos a DTO
            var notificationDto = new NotificationDto
            {
                UserId = userId,
                Title = title,
                Message = message,
                IsRead = false,
                Type = type
            };

            // Pasar datos a Notification
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                IsRead = false,
                Type = type
            };

            //Poner Notification en Repository
            try
            {
                await _notificacionRepository.InsertAsync(notification);
                _logger.LogInformation("Notification inserted in notificationRepository");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error creating and sending notifications");
                throw;
            }
            //To send notification only with types that user selected
            var notificationTypes = _notificationStrategies.Where(n => n.CanSend(type));
            _logger.LogInformation("Tipos de notificacion del usuario obtenidos");
            foreach (var notificationType in notificationTypes)
            {  
                try
                {
                    await notificationType.SendNotificationAsync(notificationDto);
                    _logger.LogInformation("Notificaciones enviadas: {NotificationType}", notificationType.GetType().Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al enviar notificaciones");
                    throw;
                }
            }
        }
        //show unread notifications on screen
        public List<NotificationDto> ShowNotifications(int userId)
        {
            _logger.LogInformation("Showing user's notifications");
            try
            {
                var notifications = _notificacionRepository.GetUnreadNotificationsAsync(userId).Result;
                _logger.LogInformation("Notifications retrieved");
                var notificationsDto = notifications.Select(n => new NotificationDto
                {
                    Message = n.Message,
                    UserId = n.UserId,
                    IsRead = n.IsRead,
                    Title = n.Title,
                    Type = n.Type
                }).ToList();
                _logger.LogInformation("Notifications converted to DTO");
                return notificationsDto;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error showing notifications");
                throw;
            }
        }
    }
}