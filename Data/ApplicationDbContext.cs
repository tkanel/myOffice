using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myITOffice.Models;

namespace myITOffice.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ItNote> ItNotes { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<OfficeUser> OfficeUsers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<TelephoneLine> TelephoneLines { get; set; }
        public DbSet<LineType> LineTypes { get; set; }
        public DbSet<HelpDesk> HelpDesks { get; set; }
        public DbSet<LoginDetail> LoginDetails { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<RackPort> RackPorts { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<MySecretPassword> MySecretPasswords { get; set; }

        internal object FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }





}
