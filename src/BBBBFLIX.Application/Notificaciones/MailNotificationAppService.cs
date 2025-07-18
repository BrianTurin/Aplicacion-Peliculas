using BBBBFLIX.Notificaciones.Strategies;
using BBBBFLIX.Notifications;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BBBBFLIX.Notificaciones
{
    public class MailNotificationAppService : INotificationStrategy
    {
        private readonly ILogger<MailNotificationAppService> _logger;

        public MailNotificationAppService(ILogger<MailNotificationAppService> logger)
        {
            _logger = logger;
        }
        //check which type of notification can be sent
        public bool CanSend(NotificationType type)
        {
            _logger.LogInformation("Verificando que el tipo de notificación puede ser enviada al usuario");
            return NotificationType.Email == type;
        }

        //method for sending email notifications
        public async Task SendNotificationAsync(NotificationDto notificationDto)
        {
            var notification = new Notification
            {
                UserId = notificationDto.UserId,
                Type = notificationDto.Type,
                IsRead = false,
                CreatedDate = notificationDto.CreatedDate,
                Title = notificationDto.Title,
                Message = notificationDto.Message
            };

            try
            {
                _logger.LogInformation("Enviando notificación por correo electrónico al {Usuario}", notification.UserId);
                _logger.LogInformation("Asunto: {Asunto}", notification.Title);
                string subject = notification.Title;
                string body = notification.Message;
                var fromAddress = new MailAddress("your-mail@example.com", "your-name");
                const string fromPassword = "your-password";
                var toAddress = new MailAddress("user@example.com", "user-name");

                var smtp = new SmtpClient
                {
                    Host = "smtp.example.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    await smtp.SendMailAsync(message);
                }
                _logger.LogInformation("Notificación enviada por correo electrónico al {Usuario}", notification.UserId);
            }
            catch
            {
                _logger.LogError("Error al enviar correo electrónico");
                throw new Exception("Error sending email");
            }
        }
    }
}
