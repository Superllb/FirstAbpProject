using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FirstAbpProject.Clients.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Clients
{
    public interface IClientAppService: IAsyncCrudAppService<ClientDto, int, FirstAbpProjectPagedResultRequestDto, CreateClientDto, UpdateClientDto>
    {
        Task<ClientDto> GetClientByIdAsync(int id);
        ListResultDto<ClientDto> GetAllClients();
    }
}
