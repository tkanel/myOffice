using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class HelpDesk
    {
        //PINAKAS MANY (WITH Category-DeviceType-User)
        public int Id { get; set; }


        [Required]
        [Display(Name = "Description")]
        [StringLength(100)]
        public string Descr { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedOn { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ResolvedOn { get; set; }



        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public string  HelpDeskAttachement { get; set; }


        //Foregin Key and Navigation Property
        public int CategoryId { get; set; }

        public int? DeviceTypeId { get; set; }

        public int? OfficeUserId { get; set; }

        public int? DeviceId { get; set; }

        public Category Category { get; set; }
        public DeviceType DeviceType { get; set; }

        public Device Device { get; set; }
        public OfficeUser OfficeUser { get; set; }



    }
}
