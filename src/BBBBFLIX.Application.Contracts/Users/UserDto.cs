using BBBBFLIX.Watchlists;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Users
{
    public class UserDto : EntityDto<Guid> // Cambia `int` a `Guid`
    {
        public required string Username { get; set; } // Nombre de usuario
        public required string Email { get; set; } // Correo electrónico
        public required string Password { get; set; } // Contraseña (considerar métodos seguros para manejar contraseñas)
        public DateTime CreatedAt { get; set; } // Fecha de creación
        public DateTime UpdatedAt { get; set; } // Fecha de actualización
        public required WatchlistDto Watchlist { get; set; } // DTO para la lista de seguimiento
        public bool IsActive { get; set; }
    }
}
