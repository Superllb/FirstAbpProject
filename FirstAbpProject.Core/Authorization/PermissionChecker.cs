using Abp.Authorization;
using FirstAbpProject.Authorization.Roles;
using FirstAbpProject.Authorization.Users;

namespace FirstAbpProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
