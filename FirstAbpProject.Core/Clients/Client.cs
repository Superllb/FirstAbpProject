using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Clients
{
    /// <summary>
    /// Represents a client entity
    /// </summary>
    [Table("Clients")]
    public class Client : Entity<int>, IHasCreationTime
    {
        /// <summary>
        /// Unique code of the client
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// virtual keyword enables lazy loading function of code first
        /// </summary>
        public virtual string Name { get; set; }

        public virtual string NameEn { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual long CreatorUserId { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }

        public virtual long? DeleteUserId { get; set; }

        public Client()
        {
            CreationTime = DateTime.UtcNow;
            IsActive = true;
            IsDeleted = false;
        }
    }
}
