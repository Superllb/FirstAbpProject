using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using FirstAbpProject.EntityFramework;

namespace FirstAbpProject.Migrator
{
    [DependsOn(typeof(FirstAbpProjectDataModule))]
    public class FirstAbpProjectMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<FirstAbpProjectDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}