using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject
{
    //Inherited Request DTO Class
    public class FirstAbpProjectPagedResultRequestDto : PagedResultRequestDto
    {
        public virtual string Filter { get; set; }
        public virtual bool IsNotPaged { get; set; }
    }
}
