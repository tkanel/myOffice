using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class RackPort
    {
        public int Id { get; set; }

        [Required]
        public int Rack { get; set; }

        [Required]
        public int Port { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public int RackId { get; set; }

    }
}
