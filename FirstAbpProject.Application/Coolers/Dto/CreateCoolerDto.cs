using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Coolers.Dto
{
    [AutoMapTo(typeof(Cooler))]
    public class CreateCoolerDto
    {
        [Required]
        public virtual int StoreId { get; set; }

        [Required]
        public virtual int SlothId { get; set; }

        public virtual string CoolerType { get; set; }

        public virtual string CoolerCode { get; set; }

        [Required]
        public virtual DataType DataType { get; set; }

        public virtual bool IsOnline { get; set; }

        public virtual bool IsQa { get; set; }
    }
}
