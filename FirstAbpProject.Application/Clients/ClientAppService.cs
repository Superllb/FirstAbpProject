using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Localization;
using FirstAbpProject.Clients.Dto;

namespace FirstAbpProject.Clients
{
    public class ClientAppService : AsyncCrudAppService<Client, ClientDto, int, FirstAbpProjectPagedResultRequestDto, CreateClientInput, UpdateClientInput>, IClientAppService
    {
        private readonly IRepository<Client, int> _clientRepository;
        private readonly ILocalizationManager _localizationManager;

        public ClientAppService(IRepository<Client, int> clientRepository, 
            ILocalizationManager localizationManager)
            : base(clientRepository)
        {
            LocalizationSourceName = FirstAbpProjectConsts.LocalizationSourceName;
            _clientRepository = clientRepository;
            _localizationManager = localizationManager;
        }

        public override Task<ClientDto> Create(CreateClientInput input)
        {
            return base.Create(input);
        }

        public override Task Delete(EntityDto<int> input)
        {
            return base.Delete(input);
        }

        public override Task<ClientDto> Get(EntityDto<int> input)
        {
            return base.Get(input);
        }

        public override Task<PagedResultDto<ClientDto>> GetAll(FirstAbpProjectPagedResultRequestDto input)
        {
            return base.GetAll(input);
        }

        public List<ClientDto> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> GetClientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<ClientDto> Update(UpdateClientInput input)
        {
            return base.Update(input);
        }

        protected override IQueryable<Client> ApplySorting(IQueryable<Client> query, FirstAbpProjectPagedResultRequestDto input)
        {
            return base.ApplySorting(query, input);
        }

        protected override Client MapToEntity(CreateClientInput createInput)
        {
            return base.MapToEntity(createInput);
        }

        protected override void MapToEntity(UpdateClientInput updateInput, Client entity)
        {
            base.MapToEntity(updateInput, entity);
        }
    }
}
