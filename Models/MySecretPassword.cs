using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myITOffice.Models
{
    public class MySecretPassword
    {

        public int Id { get; set; }

        [StringLength(400)]
        public string Name { get; set; }
        public string URL { get; set; }

        [Required]
        [StringLength(400)]
        public string Username { get; set; }

        [Required]
        [StringLength(400)]
        public string Passphrase { get; set; }


    }
}
