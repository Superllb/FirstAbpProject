using System.Linq;
using FirstAbpProject.EntityFramework;
using FirstAbpProject.MultiTenancy;

namespace FirstAbpProject.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly FirstAbpProjectDbContext _context;

        public DefaultTenantCreator(FirstAbpProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
