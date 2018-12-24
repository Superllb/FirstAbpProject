using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Localization;
using AutoMapper;
using FirstAbpProject.Authorization;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Stores.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Stores
{
    public class StoreAppService : AsyncCrudAppService<Store, StoreDto, int, FirstAbpProjectPagedResultRequestDto, CreateStoreDto, UpdateStoreDto>, IStoreAppService
    {
        private readonly IRepository<Store, int> _storeRepository;
        private readonly UserManager _userManager;
        private readonly ILanguageManager _languageManager;

        public StoreAppService(IRepository<Store, int> storeRepository,
            UserManager userManager,
            ILanguageManager languageManager) 
            : base(storeRepository)
        {
            _storeRepository = storeRepository;
            _userManager = userManager;
            _languageManager = languageManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Stores)]
        public override async Task<StoreDto> Create(CreateStoreDto input)
        {
            CheckCreatePermission();
            var storeInput = input.MapTo<Store>();
            storeInput.CreatorUserId = AbpSession.UserId.GetValueOrDefault();
            storeInput.IsDeleted = false;
            var storeId = await _storeRepository.InsertAndGetIdAsync(storeInput);

            var store = _storeRepository.GetAllIncluding(c => c.Client, c => c.User).FirstOrDefault(c => c.Id == storeId);
            return MapToEntityDto(store);
        }

        [AbpAuthorize(PermissionNames.Pages_Stores)]
        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();
            var store = await _storeRepository.GetAsync(input.Id);
            store.IsDeleted = true;

            await _storeRepository.UpdateAsync(store);
        }

        [AbpAuthorize(PermissionNames.Pages_Stores)]
        public override async Task<PagedResultDto<StoreDto>> GetAll(FirstAbpProjectPagedResultRequestDto input)
        {
            var language = _languageManager.CurrentLanguage.Name;
            var query = _storeRepository.GetAllIncluding(q => q.Client, q => q.User).Where(q => !q.IsDeleted);

            if (!string.IsNullOrEmpty(input.Filter))
            {
                query = query.Where(q => q.StoreName.Contains(input.Filter) || q.StoreNameEn.Contains(input.Filter));
            }

            var totalCount = query.Count();
            List<Store> stores = new List<Store>();
            if (input.IsNotPaged)
            {
                stores = query.OrderBy(q => q.Id).ToList();
            }
            else
            {
                stores = query.OrderBy(q => q.Id).PageBy(input).ToList();
            }
            
            return new PagedResultDto<StoreDto>(
                totalCount,
                stores.Select(MapToEntityDto).ToList()
            );
        }

        [AbpAuthorize(PermissionNames.Pages_Stores)]
        public override async Task<StoreDto> Update(UpdateStoreDto input)
        {
            CheckUpdatePermission();

            var store = await _storeRepository.GetAsync(input.Id);
            MapToEntity(input, store);
            await _storeRepository.UpdateAsync(store);

            return MapToEntityDto(store);
        }

        protected override Task<Store> GetEntityByIdAsync(int id)
        {
            var store = _storeRepository.GetAllIncluding(c => c.Client, c => c.User).FirstOrDefault(t => t.Id == id);
            return Task.FromResult(store);
        }

        protected override StoreDto MapToEntityDto(Store entity)
        {
            var language = _languageManager.CurrentLanguage.Name;
            var entityDto = base.MapToEntityDto(entity);
            entityDto.ClientName = entity.Client.Name;
            entityDto.UserName = entity.User.UserName;
            if (language != "zh-CN")
            {
                entityDto.ClientName = entity.Client.NameEn;
            }
            return entityDto;
        }
    }
}
