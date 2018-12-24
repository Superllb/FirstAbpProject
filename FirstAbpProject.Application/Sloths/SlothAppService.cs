using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Localization;
using FirstAbpProject.Authorization;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Sloths.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Sloths
{
    public class SlothAppService : AsyncCrudAppService<Sloth, SlothDto, int, FirstAbpProjectPagedResultRequestDto, CreateSlothDto, UpdateSlothDto>, ISlothAppService
    {
        private readonly IRepository<Sloth, int> _slothRepository;
        private readonly UserManager _userManager;
        private readonly ILanguageManager _languageManager;

        public SlothAppService(IRepository<Sloth, int> slothRepository,
            UserManager userManager,
            ILanguageManager languageManager)
            : base(slothRepository)
        {
            _slothRepository = slothRepository;
            _userManager = userManager;
            _languageManager = languageManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Sloths)]
        public override Task<SlothDto> Create(CreateSlothDto input)
        {
            CheckCreatePermission();
            return base.Create(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Sloths)]
        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();
            var sloth = await _slothRepository.GetAsync(input.Id);
            sloth.IsDeleted = true;
            sloth.LastModificationTime = DateTime.UtcNow;

            await _slothRepository.UpdateAsync(sloth);
        }

        [AbpAuthorize(PermissionNames.Pages_Sloths)]
        public override async Task<PagedResultDto<SlothDto>> GetAll(FirstAbpProjectPagedResultRequestDto input)
        {
            CheckGetAllPermission();
            var query = _slothRepository.GetAll().Where(q => !q.IsDeleted);

            if (!string.IsNullOrEmpty(input.Filter) && int.TryParse(input.Filter, out int id))
            {
                query = query.Where(q => q.SlothId == id);
            }

            var totalCount = query.Count();
            List<Sloth> sloths = new List<Sloth>();
            if (input.IsNotPaged)
            {
                sloths = query.OrderByDescending(q => q.Id).ToList();
            }
            else
            {
                sloths = query.OrderByDescending(q => q.Id).PageBy(input).ToList();
            }

            return new PagedResultDto<SlothDto>(
                totalCount,
                sloths.Select(MapToEntityDto).ToList()
            );
        }

        [AbpAuthorize(PermissionNames.Pages_Sloths)]
        public override async Task<SlothDto> Update(UpdateSlothDto input)
        {
            CheckUpdatePermission();
            var sloth = await _slothRepository.GetAsync(input.Id);
            MapToEntity(input, sloth);
            await _slothRepository.UpdateAsync(sloth);

            return MapToEntityDto(sloth);
        }
    }
}
