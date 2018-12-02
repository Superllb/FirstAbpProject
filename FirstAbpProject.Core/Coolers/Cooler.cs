using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Clients;
using FirstAbpProject.Sloths;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Coolers
{
    [Table("Coolers")]
    public class Cooler : Entity<int>, IHasCreationTime, ISoftDelete
    {
        /// <summary>
        /// The client id in Clients table
        /// </summary>
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public virtual int ClientId { get; set; }

        /// <summary>
        /// The user id in Users table
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual long UserId { get; set; }

        /// <summary>
        /// The sloth id in Slothes table
        /// </summary>
        [ForeignKey("SlothId")]
        public virtual Sloth Sloth { get; set; }
        public virtual int SlothId { get; set; }

        public virtual string Province { get; set; }

        public virtual string City { get; set; }

        public virtual string Area { get; set; }

        public virtual string Office { get; set; }

        public virtual string Address { get; set; }

        public virtual string AddressEn { get; set; }

        public virtual string CoolerType { get; set; }

        public virtual string CoolerCode { get; set; }

        public virtual string StoreCode { get; set; }

        public virtual string StoreName { get; set; }

        public virtual string StoreNameEn { get; set; }

        /// <summary>
        /// Baidu Longitude, converted to int, for example: 121.45956256594185 × 1000000 = 121459563
        /// </summary>
        public virtual int Gpsx { get; set; }

        /// <summary>
        /// Baidu Latitude, converted to int, for example: 31.02928799588722 × 1000000 = 31029288
        /// </summary>
        public virtual int Gpsy { get; set; }

        public Status Status { get; set; }

        /// <summary>
        /// Whether to execute QA process
        /// </summary>
        public virtual bool IsQa { get; set; }

        /// <summary>
        /// Fetch QA result (true) or model run result (false), but it makes sense if IsQa is true
        /// </summary>
        public virtual bool IsQaResult { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual long CreatorUserId { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }

        public virtual long? DeleteUserId { get; set; }
    }
}
