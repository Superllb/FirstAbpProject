using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FirstAbpProject.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Sloths
{
    [Table("Sloths")]
    public class Sloth : Entity<int>, IHasCreationTime, ISoftDelete
    {
        public virtual string Name { get; set; }

        public virtual string ModelVersion { get; set; }

        public virtual int ModelType { get; set; }

        public virtual string JsonVersion { get; set; }

        public virtual string Ip { get; set; }

        public virtual int CameraCount { get; set; }

        public virtual string CameraRowsList { get; set; }

        public virtual Status Status { get; set; }

        /// <summary>
        /// TODO, from GPS module of sloth
        /// </summary>
        public virtual int? Gpsx { get; set; }

        /// <summary>
        /// TODO, from GPS module of sloth
        /// </summary>
        public virtual int? Gpsy { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }
    }
}
