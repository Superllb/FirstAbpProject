using System.Threading.Tasks;
using Abp.Application.Services;
using FirstAbpProject.Sessions.Dto;

namespace FirstAbpProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
