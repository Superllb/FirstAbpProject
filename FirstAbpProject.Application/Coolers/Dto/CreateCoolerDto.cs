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
        public virtual int ClientId { get; set; }

        [Required]
        public virtual int UserId { get; set; }

        [Required]
        public virtual int SlothId { get; set; }

        [Required]
        public virtual string Province { get; set; }

        [Required]
        public virtual string City { get; set; }

        [Required]
        public virtual string Area { get; set; }

        [Required]
        public virtual string Office { get; set; }

        [Required]
        public virtual string Address { get; set; }

        public virtual string AddressEn { get; set; }

        public virtual string CoolerType { get; set; }

        public virtual string CoolerCode { get; set; }

        public virtual string StoreCode { get; set; }

        [Required]
        public virtual string StoreName { get; set; }

        public virtual string StoreNameEn { get; set; }

        [Required]
        public virtual float Longitude { get; set; }

        [Required]
        public virtual float Latitude { get; set; }

        public virtual bool IsOnline { get; set; }

        public virtual bool IsQa { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual int? CustomerId { get; set; }

        public virtual Guid? ProjectId { get; set; }
    }
}
