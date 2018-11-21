using System.Threading.Tasks;
using Abp.Application.Services;
using FirstAbpProject.Authorization.Accounts.Dto;

namespace FirstAbpProject.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
