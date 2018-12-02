﻿using Abp.Application.Services;
using FirstAbpProject.Clients.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Clients
{
    public interface IClientAppService: IAsyncCrudAppService<ClientDto, int, FirstAbpProjectPagedResultRequestDto, CreateClientInput, UpdateClientInput>
    {
        Task<ClientDto> GetClientByIdAsync(int id);
        List<ClientDto> GetAllClients();
    }
}