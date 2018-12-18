using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Stores.Dto
{
    public class CreateStoreDto
    {
        [Required]
        public virtual int ClientId { get; set; }

        [Required]
        public virtual int UserId { get; set; }

        [Required]
        public virtual string Province { get; set; }

        [Required]
        public virtual string City { get; set; }

        [Required]
        public virtual string Area { get; set; }

        public virtual string Office { get; set; }

        public virtual string Address { get; set; }

        public virtual string AddressEn { get; set; }

        public virtual string StoreCode { get; set; }

        public virtual string StoreName { get; set; }

        public virtual string StoreNameEn { get; set; }

        [Required]
        public virtual float Longitude { get; set; }

        [Required]
        public virtual float Latitude { get; set; }
    }
}
