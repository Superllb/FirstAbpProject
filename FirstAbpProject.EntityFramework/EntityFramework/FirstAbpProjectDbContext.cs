using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using FirstAbpProject.Authorization.Roles;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.MultiTenancy;

namespace FirstAbpProject.EntityFramework
{
    public class FirstAbpProjectDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public FirstAbpProjectDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in FirstAbpProjectDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of FirstAbpProjectDbContext since ABP automatically handles it.
         */
        public FirstAbpProjectDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public FirstAbpProjectDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public FirstAbpProjectDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
