using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.UI;
using AutoMapper;
using FirstAbpProject.Authorization;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Clients.Dto;

namespace FirstAbpProject.Clients
{
    public class ClientAppService : AsyncCrudAppService<Client, ClientDto, int, FirstAbpProjectPagedResultRequestDto, CreateClientInput, UpdateClientInput>, IClientAppService
    {
        private readonly IRepository<Client, int> _clientRepository;
        private readonly UserManager _userManager;
        private readonly ILanguageManager _languageManager;

        public ClientAppService(IRepository<Client, int> clientRepository, 
            UserManager userManager,
            ILanguageManager languageManager)
            : base(clientRepository)
        {
            LocalizationSourceName = FirstAbpProjectConsts.LocalizationSourceName;
            _userManager = userManager;
            _clientRepository = clientRepository;
            _languageManager = languageManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Clients)]
        public override async Task<ClientDto> Create(CreateClientInput input)
        {
            #region Check client code naming rule
            if (input.Code.Split('.').Length != 2)
            {
                throw new UserFriendlyException(L("InvalidClientCode"));
            }
            #endregion

            if (_clientRepository.GetAll().Any(c => c.Code == input.Code))
            {
                throw new UserFriendlyException(L("InvalidCodeAndExist"));
            }

            CheckCreatePermission();
            var clientInput = input.MapTo<Client>();
            clientInput.CreatorUserId = AbpSession.UserId.GetValueOrDefault();
            var clientId = await _clientRepository.InsertAndGetIdAsync(clientInput);

            var client = _clientRepository.Get(clientId);
            var clientDto = MapToEntityDto(client);

            return clientDto;
        }

        public override async Task Delete(EntityDto<int> input)
        {
            var client = await _clientRepository.GetAsync(input.Id);
            client.IsDeleted = true;
            client.IsActive = false;
            client.DeletionTime = DateTime.UtcNow;
            client.DeleteUserId = AbpSession.UserId.GetValueOrDefault();
            await _clientRepository.UpdateAsync(client);
        }

        [AbpAuthorize(PermissionNames.Pages_Clients)]
        public override async Task<ClientDto> Get(EntityDto<int> input)
        {
            var client = await base.Get(input);
            return client;
        }

        [AbpAuthorize(PermissionNames.Pages_Clients)]
        public override async Task<PagedResultDto<ClientDto>> GetAll(FirstAbpProjectPagedResultRequestDto input)
        {
            CheckGetAllPermission();
            var language = _languageManager.CurrentLanguage.Name;
            var query = _clientRepository.GetAll().Where(q => !q.IsDeleted);
            
            if (!string.IsNullOrEmpty(input.Filter))
            {
                query = query.Where(q => q.Name.Contains(input.Filter) || q.NameEn.Contains(input.Filter));
            }
            
            var totalCount = query.Count();
            List<Client> clients = new List<Client>();
            if (input.IsNotPaged)
            {
                clients = query.OrderBy(q => q.Id).ToList();
            }
            else
            {
                clients = query.OrderBy(q => q.Id).PageBy(input).ToList();
            }

            return new PagedResultDto<ClientDto>(
                totalCount,
                Mapper.Map<List<ClientDto>>(clients)
            );
        }

        public List<ClientDto> GetAllClients()
        {
            CheckGetAllPermission();
            var language = _languageManager.CurrentLanguage.Name;
            var query = _clientRepository.GetAll().Where(q => !q.IsDeleted);

            var clients = query.OrderBy(q => q.CreationTime).ToList();

            return Mapper.Map<List<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetAsync(id);
            var clientDto = MapToEntityDto(client);

            return clientDto;
        }

        [AbpAuthorize(PermissionNames.Pages_Clients)]
        public override async Task<ClientDto> Update(UpdateClientInput input)
        {
            CheckUpdatePermission();

            var client = await _clientRepository.GetAsync(input.Id);
            MapToEntity(input, client);
            client.LastModificationTime = DateTime.UtcNow;
            client.LastModifierUserId = AbpSession.UserId.GetValueOrDefault();

            await _clientRepository.UpdateAsync(client);

            return await Get(input);
        }

        protected override IQueryable<Client> ApplySorting(IQueryable<Client> query, FirstAbpProjectPagedResultRequestDto input)
        {
            return base.ApplySorting(query, input);
        }
        
        protected override Client MapToEntity(CreateClientInput createInput)
        {
            var client = ObjectMapper.Map<Client>(createInput);
            return client;
        }

        protected override void MapToEntity(UpdateClientInput input, Client client)
        {
            ObjectMapper.Map(input, client);
        }
    }
}
