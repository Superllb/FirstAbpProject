using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Stores.Dto
{
    [AutoMapFrom(typeof(Store))]
    public class StoreDto : EntityDto<int>
    {
        public virtual int ClientId { get; set; }

        public virtual long UserId { get; set; }

        public virtual string Province { get; set; }

        public virtual string City { get; set; }

        public virtual string Area { get; set; }

        public virtual string Office { get; set; }

        public virtual string Address { get; set; }

        public virtual string StoreCode { get; set; }

        public virtual string StoreName { get; set; }

        public virtual float Longitude { get; set; }

        public virtual float Latitude { get; set; }
    }
}
