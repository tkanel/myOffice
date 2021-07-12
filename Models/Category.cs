using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class Category
    {
        //PINAKAS 1 (WITH ItNotes)
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Descr { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }



        public ICollection<ItNote> ItNotes { get; set; }
        public ICollection<HelpDesk> HelpDesks { get; set; }

    }
}
