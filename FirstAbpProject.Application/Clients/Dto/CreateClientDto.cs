using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstAbpProject.Clients.Dto
{
    [AutoMapTo(typeof(Client))]
    public class CreateClientDto
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameEn { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
