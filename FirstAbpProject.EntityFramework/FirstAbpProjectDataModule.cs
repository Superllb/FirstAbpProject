using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using FirstAbpProject.EntityFramework;

namespace FirstAbpProject
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(FirstAbpProjectCoreModule))]
    public class FirstAbpProjectDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<FirstAbpProjectDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
