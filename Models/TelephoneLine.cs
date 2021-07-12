using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class TelephoneLine
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Number { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateStart { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateExpired { get; set; }

        [StringLength(20)]
        public string SIM { get; set; }

        [StringLength(20)]
        public string PIN1 { get; set; }

        [StringLength(20)]
        public string PIN2 { get; set; }

        [StringLength(20)]
        public string PUK1 { get; set; }

        [StringLength(20)]
        public string PUK2 { get; set; }

        [StringLength(20)]
        public string Shortcut { get; set; }
       
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }





        public int? LineTypeId { get; set; }
        public LineType LineType { get; set; }







    }
}
