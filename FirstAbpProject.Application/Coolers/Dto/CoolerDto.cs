using Abp.Application.Services.Dto;
using Abp.AutoMapper;
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

        public virtual int SlothId { get; set; }

        public virtual string Province { get; set; }

        public virtual string City { get; set; }

        public virtual string Area { get; set; }

        public virtual string Office { get; set; }

        public virtual string Address { get; set; }

        public virtual string CoolerType { get; set; }

        public virtual string CoolerCode { get; set; }

        public virtual string StoreCode { get; set; }

        public virtual string StoreName { get; set; }

        public virtual float Longitude { get; set; }

        public virtual float Latitude { get; set; }

        public virtual bool IsOnline { get; set; }

        public virtual bool IsQa { get; set; }

        public virtual int? CustomerId { get; set; }

        public virtual Guid? ProjectId { get; set; }
    }
}
