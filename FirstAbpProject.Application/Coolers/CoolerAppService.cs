using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Localization;
using FirstAbpProject.Authorization;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Clients;
using FirstAbpProject.Common;
using FirstAbpProject.Coolers.Dto;
using FirstAbpProject.Helpers;
using FirstAbpProject.Sloths;
using FirstAbpProject.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Coolers
{
    public class CoolerAppService : AsyncCrudAppService<Cooler, CoolerDto, int, FirstAbpProjectPagedResultRequestDto, CreateCoolerDto, UpdateCoolerDto>, ICoolerAppService
    {
        private readonly IRepository<Cooler, int> _coolerRepository;
        private readonly IRepository<Store, int> _storeRepository;
        private readonly IRepository<Sloth, int> _slothRepository;
        private readonly UserManager _userManager;
        private readonly ILanguageManager _languageManager;

        public CoolerAppService(IRepository<Cooler, int> coolerRepository,
            IRepository<Store, int> storeRepository,
            IRepository<Sloth, int> slothRepository,
            UserManager userManager, 
            ILanguageManager languageManager)
            : base(coolerRepository)
        {
            _coolerRepository = coolerRepository;
            _storeRepository = storeRepository;
            _slothRepository = slothRepository;
            _userManager = userManager;
            _languageManager = languageManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Coolers)]
        public override async Task<CoolerDto> Create(CreateCoolerDto input)
        {
            CheckCreatePermission();
            var coolerInput = input.MapTo<Cooler>();
            Store store = _storeRepository.Get(input.StoreId);
            coolerInput.ClientId = store.ClientId;
            coolerInput.UserId = store.UserId;
            coolerInput.Status = Status.Alive;
            coolerInput.IsDeleted = false;

            _slothRepository.InsertAndGetId(new Sloth {
                Id = input.SlothId,
                ModelType = 0,
                CameraCount = 2,
                Status = Status.Alive,
                IsDeleted = false,
                CreationTime = DateTime.UtcNow
            });

            var coolerId = await _coolerRepository.InsertAndGetIdAsync(coolerInput);
            var cooler = _coolerRepository.GetAllIncluding(c => c.Client, c => c.User).FirstOrDefault(c => c.Id == coolerId);
            return MapToEntityDto(cooler);
        }

        [AbpAuthorize(PermissionNames.Pages_Coolers)]
        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();
            var cooler = await _coolerRepository.GetAsync(input.Id);
            cooler.IsDeleted = true;

            await _coolerRepository.UpdateAsync(cooler);
        }

        [AbpAuthorize(PermissionNames.Pages_Coolers)]
        public override async Task<PagedResultDto<CoolerDto>> GetAll(FirstAbpProjectPagedResultRequestDto input)
        {
            var language = _languageManager.CurrentLanguage.Name;
            var query = _coolerRepository.GetAllIncluding(q => q.Client, q => q.User).Where(q => !q.IsDeleted);

            if (!string.IsNullOrEmpty(input.Filter))
            {
                query = query.Where(q => q.CoolerType.Contains(input.Filter) || q.CoolerCode.Contains(input.Filter));
            }
            query = query.OrderBy(t => t.ClientId).ThenBy(t => t.UserId).OrderBy(q => q.Id);

            var totalCount = query.Count();
            List<Cooler> coolers = new List<Cooler>();
            if (input.IsNotPaged)
            {
                coolers = query.ToList();
            }
            else
            {
                coolers = query.PageBy(input).ToList();
            }

            return new PagedResultDto<CoolerDto>(
                totalCount,
                coolers.Select(MapToEntityDto).ToList()
            );
        }

        public List<EnumDetail> GetDataTypes()
        {
            return EnumHelper.GetEnumDetail<DataType>();
        }

        [AbpAuthorize(PermissionNames.Pages_Coolers)]
        public override async Task<CoolerDto> Update(UpdateCoolerDto input)
        {
            CheckUpdatePermission();
            var cooler = await _coolerRepository.GetAsync(input.Id);
            MapToEntity(input, cooler);
            await _coolerRepository.UpdateAsync(cooler);

            return MapToEntityDto(cooler);
        }

        protected override CoolerDto MapToEntityDto(Cooler entity)
        {
            var language = _languageManager.CurrentLanguage.Name;
            List<EnumDetail> dataTypes = EnumHelper.GetEnumDetail<DataType>();
            var entityDto = base.MapToEntityDto(entity);
            entityDto.ClientName = entity.Client.Name;
            entityDto.UserName = entity.User.UserName;
            entityDto.Longitude = entity.Store.Longitude;
            entityDto.Latitude = entity.Store.Latitude;
            entityDto.DataType = EnumHelper.GetEnumDetail(entity.DataType).Description;
            if (language != "zh-CN")
            {
                entityDto.ClientName = entity.Client.NameEn;
                entityDto.DataType = EnumHelper.GetEnumDetail(entity.DataType).Name;
            }
            return entityDto;
        }
    }
}
