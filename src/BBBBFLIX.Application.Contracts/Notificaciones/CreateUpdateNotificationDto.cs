using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBBFLIX.Notifications
{
    public class CreateUpdateNotificationDto
    {
        public Guid UsuarioId { get; set; }  // ID del usuario asociado a la notificación
        public required string Mensaje { get; set; }  // Mensaje de la notificación
        public bool Leido { get; set; }  // Estado de lectura de la notificación
    }
}
