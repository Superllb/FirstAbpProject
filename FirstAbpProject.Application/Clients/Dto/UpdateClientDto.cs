using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Clients.Dto
{
    [AutoMapTo(typeof(Client))]
    public class UpdateClientDto : EntityDto<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string NameEn { get; set; }

        public bool IsActive { get; set; }
    }
}
