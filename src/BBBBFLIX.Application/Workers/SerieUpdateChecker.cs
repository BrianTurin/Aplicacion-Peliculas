using BBBBFLIX.Notificaciones.Strategies;
using BBBBFLIX.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BBBBFLIX.Workers
{
    public class SerieUpdateService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SerieUpdateService> _logger;

        public SerieUpdateService(IServiceScopeFactory scopeFactory, ILogger<SerieUpdateService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SerieUpdateService iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationAppService>();

                    await notificationService.CreateSendNotificationsAsync(
                        userId: 1,
                        title: "Actualización de series",
                        message: "Se detectaron nuevas actualizaciones en tus series.",
                        type: NotificationType.Email
                    );

                    _logger.LogInformation("Notificación enviada correctamente.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al enviar notificaciones desde SerieUpdateService.");
                }

                // Esperar 1 hora (o lo que necesites)
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
