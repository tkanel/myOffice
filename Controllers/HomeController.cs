using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myITOffice.Data;

namespace myITOffice.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            dynamic mymodel = new ExpandoObject();

            mymodel.HelpDesks = _context.HelpDesks
               .Where(b => !b.ResolvedOn.HasValue)
               .Take(10)
               .ToList();

            mymodel.Users = _context.OfficeUsers
              .OrderBy(s => s.Name)
              .Take(10)
              .ToList();

            mymodel.Categories = _context.Categories
                .OrderBy(s => s.Descr)
                .Take(10)
                .ToList();

            mymodel.DeviceTypes = _context.DeviceTypes
                .OrderBy(s => s.Description)
                .Take(10)
                .ToList();

            mymodel.Devices = _context.Devices
               .OrderBy(s => s.Name)
               .Take(10)
               .ToList();

            mymodel.LineTypes = _context.LineTypes
                .OrderBy(s => s.Description)
                .Take(10)
                .ToList();

            mymodel.LoginDetails = _context.LoginDetails
               .OrderBy(s => s.Description)
               .Take(10)
               .ToList();

            mymodel.TelephoneLines = _context.TelephoneLines
                .OrderBy(s => s.Number)
                .Take(50)
                .ToList();

            ViewBag.OpenTickets = _context.HelpDesks
                .Count(p => !p.ResolvedOn.HasValue);

            ViewBag.Users = _context.OfficeUsers
                .Count();

            ViewBag.Devices = _context.Devices
                .Count();

            ViewBag.DeviceTypes = _context.DeviceTypes
                .Count();

            ViewBag.TelLines = _context.TelephoneLines
                .Count();

            ViewBag.Categories = _context.Categories
                .Count();

            ViewBag.LineTypes = _context.LineTypes
                .Count();

            ViewBag.Logins = _context.LoginDetails
                .Count();

            return View(mymodel);
        }
    }
}