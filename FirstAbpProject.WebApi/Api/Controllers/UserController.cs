using Abp.Application.Services.Dto;
using Abp.WebApi.Controllers;
using FirstAbpProject.Roles.Dto;
using FirstAbpProject.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FirstAbpProject.Api.Controllers
{
    public class UserController : AbpApiController
    {
        private readonly IUserAppService _userAppService;

        public UserController(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        [Route("api/user/getRoles")]
        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            return await _userAppService.GetRoles();
        }
    }
}
