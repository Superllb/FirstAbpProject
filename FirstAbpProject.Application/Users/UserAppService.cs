using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Localization;
using AutoMapper;
using FirstAbpProject.Authorization;
using FirstAbpProject.Authorization.Roles;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Clients;
using FirstAbpProject.Roles.Dto;
using FirstAbpProject.Users.Dto;
using Microsoft.AspNet.Identity;

namespace FirstAbpProject.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Client, int> _clientRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly ILanguageManager _languageManager;

        public UserAppService(
            IRepository<User, long> repository,
            IRepository<Client, int> clientRepository,
            UserManager userManager, 
            IRepository<Role> roleRepository, 
            RoleManager roleManager,
            ILanguageManager languageManager)
            : base(repository)
        {
            _userManager = userManager;
            _userRepository = repository;
            _clientRepository = clientRepository;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _languageManager = languageManager;
        }

        public override async Task<PagedResultDto<UserDto>> GetAll(PagedResultRequestDto input)
        {
            CheckGetAllPermission();
            var language = _languageManager.CurrentLanguage.Name;
            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            var users = entities.Select(MapToEntityDto).ToList();
            users.Select(t => {
                var entity = entities.FirstOrDefault(u => u.Id == t.Id);
                if (entity.LeaderId.HasValue)
                {
                    t.LeaderName = entities.FirstOrDefault(u => u.Id == entity.LeaderId.Value).UserName; 
                }
                t.ClientName = entity.Client != null ? (language == "zh-CN" ? entity.Client.Name : entity.Client.NameEn) : string.Empty;
                return t;
            }).ToList();

            return new PagedResultDto<UserDto>(
                totalCount,
                users
            );
        }

        public override async Task<UserDto> Get(EntityDto<long> input)
        {
            var language = _languageManager.CurrentLanguage.Name;
            var user = await base.Get(input);
            var entity = await base.GetEntityByIdAsync(user.Id);
            if (entity.LeaderId.HasValue)
            {
                var leader = await base.GetEntityByIdAsync(entity.LeaderId.Value);
                user.LeaderName = leader.UserName;
            }
            if (entity.Client != null)
            {
                user.ClientName = entity.Client != null ? (language == "zh-CN" ? entity.Client.Name : entity.Client.NameEn) : null;
            }
            var userRoles = await _userManager.GetRolesAsync(user.Id);
            user.Roles = userRoles.Select(ur => ur).ToArray();
            return user;
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();
            var language = _languageManager.CurrentLanguage.Name;
            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.RoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await _userManager.CreateAsync(user));

            await CurrentUnitOfWork.SaveChangesAsync();

            var entity = await base.GetEntityByIdAsync(user.Id);
            var userDto = MapToEntityDto(entity);
            if (entity.LeaderId.HasValue)
            {
                var leader = await base.GetEntityByIdAsync(entity.LeaderId.Value);
                userDto.LeaderName = leader.UserName;
            }
            if (entity.Client != null)
            {
                userDto.ClientName = entity.Client != null ? (language == "zh-CN" ? entity.Client.Name : entity.Client.NameEn) : null;
            }
            return userDto;
        }

        public override async Task<UserDto> Update(UpdateUserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            return user;
        }

        protected override void MapToEntity(UpdateUserDto input, User user)
        {
            ObjectMapper.Map(input, user);
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles, x => x.Client);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = Repository.GetAllIncluding(x => x.Roles).FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        [AbpAuthorize(PermissionNames.Pages_Clients)]
        public ListResultDto<UserDto> GetUsersByClientId(int clientId)
        {
            CheckGetAllPermission();
            var language = _languageManager.CurrentLanguage.Name;
            var client = _clientRepository.Get(clientId);
            var users = _userRepository.GetAll()
                .Where(t => !t.IsDeleted && t.ClientId == clientId)
                .OrderBy(t => t.Id).ToList();
            
            List<UserDto> useDtos = ObjectMapper.Map<List<UserDto>>(users);
            useDtos = useDtos.Select(t => { t.ClientName = language == "zh-CN" ? client.Name : client.NameEn; return t; }).ToList();

            return new ListResultDto<UserDto>(useDtos);
        }
    }
}