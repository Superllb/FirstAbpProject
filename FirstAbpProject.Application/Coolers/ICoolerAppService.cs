using Abp.Application.Services;
using FirstAbpProject.Coolers.Dto;
using FirstAbpProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Coolers
{
    public interface ICoolerAppService : IAsyncCrudAppService<CoolerDto, int, FirstAbpProjectPagedResultRequestDto, CreateCoolerDto, UpdateCoolerDto>
    {
        List<EnumDetail> GetDataTypes();
    }
}
