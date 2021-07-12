using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myITOffice.Models;

namespace myITOffice
{
    public sealed class ImportMap : ClassMap<MySecretPassword>
    {


        public ImportMap()
        {

            //Map(m => m.Id).Name("id");Automatically increment
            Map(m => m.Name).Name("Name");
            Map(m => m.URL).Name("URL");
            Map(m => m.Username).Name("Username");
            Map(m => m.Passphrase).Name("Passphrase");





        }

        







    }
}
