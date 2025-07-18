using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BBBBFLIX.Users
{
    public interface IUserAppService : ICrudAppService<
        UserDto,               // La entidad (dominio)
        Guid,                   // Tipo de la clave primaria
        PagedAndSortedResultRequestDto, // Para paginación y ordenamiento
        CreateUpdateUserDto,   // DTO para creación y actualización
        CreateUpdateUserDto    // DTO para creación y actualización
    >
    {
        Task<UserDto> FindByUsernameAsync(string username);
        Task<List<UserDto>> GetActiveUsersAsync();
    }
}