using BBBBFLIX.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace BBBBFLIX.Application.Users
{
    public class AppUserAppService : ApplicationService, IUserAppService
    {
        private readonly IIdentityUserAppService _identityUserAppService;

        public AppUserAppService(IIdentityUserAppService identityUserAppService)
        {
            _identityUserAppService = identityUserAppService;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _identityUserAppService.GetAsync(id);
            return ObjectMapper.Map<IdentityUserDto, UserDto>(user);
        }

        public async Task<PagedResultDto<UserDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            // Crear un objeto de GetIdentityUsersInput con los valores de paginación y ordenación
            var identityInput = new GetIdentityUsersInput
            {
                SkipCount = input.SkipCount,  // Mapea la paginación
                MaxResultCount = input.MaxResultCount,  // Mapea el número máximo de resultados
                Sorting = input.Sorting,  // Mapea el criterio de ordenación
            };

            // Llama al servicio con el tipo adecuado
            var users = await _identityUserAppService.GetListAsync(identityInput);

            // Mapea los usuarios obtenidos a UserDto
            return ObjectMapper.Map<PagedResultDto<IdentityUserDto>, PagedResultDto<UserDto>>(users);
        }
        public async Task<UserDto> CreateAsync(CreateUpdateUserDto input)
        {
            var identityUserCreateDto = ObjectMapper.Map<CreateUpdateUserDto, IdentityUserCreateDto>(input);
            var user = await _identityUserAppService.CreateAsync(identityUserCreateDto);
            return ObjectMapper.Map<IdentityUserDto, UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(Guid id, CreateUpdateUserDto input)
        {
            var identityUserUpdateDto = ObjectMapper.Map<CreateUpdateUserDto, IdentityUserUpdateDto>(input);
            var user = await _identityUserAppService.UpdateAsync(id, identityUserUpdateDto);
            return ObjectMapper.Map<IdentityUserDto, UserDto>(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _identityUserAppService.DeleteAsync(id);
        }
        public async Task<UserDto> FindByUsernameAsync(string username)
        {
            var identityInput = new GetIdentityUsersInput
            {
                MaxResultCount = 1,
                Filter = username
            };

            var users = await _identityUserAppService.GetListAsync(identityInput);
            var user = users.Items.FirstOrDefault(u => u.UserName == username);
            return user != null ? ObjectMapper.Map<IdentityUserDto, UserDto>(user) : null;
        }
        public async Task<List<UserDto>> GetActiveUsersAsync()
        {
            // Crear un objeto GetIdentityUsersInput en lugar de PagedAndSortedResultRequestDto
            var input = new GetIdentityUsersInput
            {
                MaxResultCount = 1000, // Define un límite de resultados según sea necesario
                Filter = string.Empty // Puedes especificar un filtro aquí si es necesario
            };

            var users = await _identityUserAppService.GetListAsync(input);
            var activeUsers = users.Items.Where(u => u.IsActive).ToList();
            return ObjectMapper.Map<List<IdentityUserDto>, List<UserDto>>(activeUsers);
        }
    }
}