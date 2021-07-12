using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class Rack
    {
        public int Id { get; set; }
        public string Brand  { get; set; }

        [Required]
        public int AssetNr { get; set; }

        [Required]
        public int PortsNr { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }


        public ICollection<RackPort> RackPorts { get; set; }


    }
}
