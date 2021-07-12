using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class OfficeUser
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(20)]
        public string InternalPhone { get; set; }

        [StringLength(20)]
        public string MobilePhone { get; set; }

        [StringLength(20)]
        public string Company { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }



        public Device Device { get; set; }

        public ICollection<ItNote> ItNotes { get; set; }
        public ICollection<HelpDesk> HelpDesks { get; set; }


    }
}
