using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using Abp.Timing;
using FirstAbpProject.Authorization.Roles;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Coolers;
using FirstAbpProject.Coolers.Dto;
using FirstAbpProject.Roles.Dto;
using FirstAbpProject.Users.Dto;

namespace FirstAbpProject
{
    [DependsOn(typeof(FirstAbpProjectCoreModule), typeof(AbpAutoMapperModule))]
    public class FirstAbpProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // Set UTC time as default
            Clock.Provider = ClockProviders.Utc;

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateCoolerDto, Cooler>()
                    .ForMember(x => x.Gpsx, opt => opt.MapFrom(src => (int)src.Longitude * 1000000))
                    .ForMember(x => x.Gpsy, opt => opt.MapFrom(src => (int)src.Latitude * 1000000));

                cfg.CreateMap<Cooler, CoolerDto>()
                    .ForMember(x => x.Longitude, opt => opt.MapFrom(src => (float)src.Gpsx / 1000000))
                    .ForMember(x => x.Latitude, opt => opt.MapFrom(src => (float)src.Gpsy / 1000000));
            });
        }
    }
}
