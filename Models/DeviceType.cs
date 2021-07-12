using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class DeviceType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }



        public ICollection<ItNote> ItNotes { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<HelpDesk> HelpDesks { get; set; }

    }
}
