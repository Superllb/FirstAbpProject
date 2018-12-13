using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Coolers.Dto
{
    [AutoMapTo(typeof(Cooler))]
    public class UpdateCoolerDto : EntityDto<int>
    {

    }
}
