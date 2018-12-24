using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FirstAbpProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Coolers.Dto
{
    [AutoMapFrom(typeof(Cooler))]
    public class CoolerDto : EntityDto<int>
    {
        public virtual string ClientName { get; set; }
        
        public virtual string UserName { get; set; }

        public virtual int StoreId { get; set; }

        public virtual int SlothId { get; set; }

        public virtual string CoolerType { get; set; }

        public virtual string CoolerCode { get; set; }

        public virtual string DataType { get; set; }
        
        public virtual float Longitude { get; set; }
        
        public virtual float Latitude { get; set; }

        public virtual Status Status { get; set; }

        public virtual bool IsOnline { get; set; }

        public virtual bool IsQa { get; set; }
    }
}
