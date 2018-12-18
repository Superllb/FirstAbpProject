using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Localization;
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
        private readonly UserManager _userManager;
        private readonly ILanguageManager _languageManager;

        public StoreAppService(IRepository<Store, int> storeRepository,
            UserManager userManager,
            ILanguageManager languageManager) 
            : base(storeRepository)
        {
            _userManager = userManager;
            _languageManager = languageManager;
        }
    }
}
