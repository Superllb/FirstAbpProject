using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FirstAbpProject.Authorization.Users;
using FirstAbpProject.Clients;
using FirstAbpProject.Sloths;
using FirstAbpProject.Stores;
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
        /// The store id in Stores table
        /// </summary>
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
        public virtual int StoreId { get; set; }

        /// <summary>
        /// The sloth id in Slothes table
        /// </summary>
        [ForeignKey("SlothId")]
        public virtual Sloth Sloth { get; set; }
        public virtual int SlothId { get; set; }

        public virtual string CoolerType { get; set; }

        public virtual string CoolerCode { get; set; }

        public Status Status { get; set; }

        /// <summary>
        /// Run local model(false) or cloud model(true), default true
        /// </summary>
        public virtual bool IsOnline { get; set; }

        /// <summary>
        /// Whether to execute QA process
        /// </summary>
        public virtual bool IsQa { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual long CreatorUserId { get; set; }
    }
}
