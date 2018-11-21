using System.Threading.Tasks;
using Abp.Application.Services;
using FirstAbpProject.Configuration.Dto;

namespace FirstAbpProject.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}