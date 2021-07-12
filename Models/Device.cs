using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class Device
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [StringLength(100)]
        public string IPAddress1 { get; set; }

        [StringLength(100)]
        public string IPAddress2 { get; set; }

        [StringLength(100)]
        public string IPAddress3 { get; set; }

        [StringLength(100)]
        public string IPAddress4 { get; set; }

        [StringLength(100)]
        public string IPAddress5 { get; set; }

        [StringLength(20)]
        public string MAC { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public string DeviceAttachement { get; set; }

        public int? DeviceTypeId { get; set; }

        public int? OfficeUserId { get; set; }



        public OfficeUser OfficeUser { get; set; }

        public DeviceType DeviceType { get; set; }

        public ICollection<ItNote> ItNotes { get; set; }
        public ICollection<HelpDesk> HelpDesks { get; set; }

    }
}
