using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FirstAbpProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Sloths.Dto
{
    [AutoMapFrom(typeof(Sloth))]
    public class SlothDto : EntityDto<int>
    {
        public virtual int CoolerId { get; set; }

        public virtual int SlothId { get; set; }

        public virtual string ModelVersion { get; set; }

        public virtual int ModelType { get; set; }

        public virtual string JsonVersion { get; set; }

        public virtual string Ip { get; set; }

        public virtual Status Status { get; set; }
    }
}
