using Abp.Application.Services;
using FirstAbpProject.Sloths.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Sloths
{
    public interface ISlothAppService : IAsyncCrudAppService<SlothDto, int, FirstAbpProjectPagedResultRequestDto, CreateSlothDto, UpdateSlothDto>
    {
    }
}
