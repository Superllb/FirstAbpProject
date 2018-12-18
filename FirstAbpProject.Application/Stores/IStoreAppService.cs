using Abp.Application.Services;
using FirstAbpProject.Stores.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Stores
{
    public interface IStoreAppService : IAsyncCrudAppService<StoreDto, int, FirstAbpProjectPagedResultRequestDto, CreateStoreDto, UpdateStoreDto>
    {
    }
}
