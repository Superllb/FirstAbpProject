using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Localization;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Coolers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Coolers
{
    public class CoolerAppService : AsyncCrudAppService<Cooler, CoolerDto, int, FirstAbpProjectPagedResultRequestDto, CreateCoolerDto, UpdateCoolerDto>, ICoolerAppService
    {
        private readonly IRepository<Cooler, int> _clientRepository;
        private readonly UserManager _userManager;
        private readonly ILanguageManager _languageManager;

        public CoolerAppService(IRepository<Cooler, int> coolerRepository, 
            UserManager userManager, 
            ILanguageManager languageManager)
            : base(coolerRepository)
        {
            _clientRepository = coolerRepository;
            _userManager = userManager;
            _languageManager = languageManager;
        }
    }
}
