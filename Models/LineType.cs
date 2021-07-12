using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class LineType
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string  Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string  Notes { get; set; }

        public ICollection<TelephoneLine> TelephoneLines { get; set; }


    }
}
