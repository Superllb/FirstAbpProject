using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.MultiTenancy;
using FirstAbpProject.Users;
using Microsoft.AspNet.Identity;

namespace FirstAbpProject
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class FirstAbpProjectAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected FirstAbpProjectAppServiceBase()
        {
            LocalizationSourceName = FirstAbpProjectConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}