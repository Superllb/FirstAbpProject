using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FirstAbpProject.Roles.Dto;
using FirstAbpProject.Users.Dto;

namespace FirstAbpProject.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
        ListResultDto<UserDto> GetUsersByClientId(int clientId);
    }
}