using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Clients.Dto
{
    [AutoMapFrom(typeof(Client))]
    public class ClientDto : EntityDto<int>
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameEn { get; set; }

        public bool IsActive { get; set; }
    }
}
