using BBBBFLIX.Series;
using BBBBFLIX.WatchLists;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using BBBBFLIX.Notifications;
using Volo.Abp.Identity;

namespace BBBBFLIX.Users
{
    public class User : IdentityUser // `IdentityUser` ya utiliza `Guid` como clave primaria
    {
        public string Username { get; set; }
        public string Email { get; set; }

        // Nota: Usa un enfoque seguro para manejar la contraseña (p. ej., ASP.NET Identity para hashing).
        public string PasswordHash { get; set; }

        public string Watchlist { get; set; }
        public List<Notification> Notifications { get; set; } = new List<Notification>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        // Constructor vacío requerido por el ORM
        protected User() { }

        // Constructor con parámetros
        public User(Guid id, string username, string email, string passwordHash, string watchlist)
            : base(id, username, email)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Watchlist = watchlist;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsActive = true;
        }
    }
}