using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace FirstAbpProject.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(FirstAbpProjectApplicationModule))]
    public class FirstAbpProjectWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            // Allow CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            Configuration.Modules.AbpWebApi().HttpConfiguration.EnableCors(cors);

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(FirstAbpProjectApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM--dd HH:mm:ss";

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
