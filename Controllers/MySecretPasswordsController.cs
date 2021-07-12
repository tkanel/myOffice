using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myITOffice.Data;
using myITOffice.Models;


namespace myITOffice.Controllers
{
    [Authorize]
    public class MySecretPasswordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MySecretPasswordsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: MySecretPasswords
        public  IActionResult Index()
        {

            ViewBag.myPin = false;

            return View();

        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] string myPIN)
        {

            if (myPIN == "5983")
            {
                ViewBag.myPin = true;
                ViewBag.myHiddenDivs = "123456";
                return View(await _context.MySecretPasswords.ToListAsync());


            }
            else
            {
                ViewBag.myPin = false;
                return View();

            }

            
        }




        //IMPORT GET
        public IActionResult Import()
        {

            

            return View();

        }


        //IMPORT POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Import([FromForm] string file)
        {

            //var myCSV = Path.GetFullPath(file);

            try
            {
                var reader = new StreamReader(file);
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                //csv.Configuration.HasHeaderRecord = false;
                //csv.Configuration.RegisterClassMap<ImportMap>(); Μου δίνει λάθος
                //csv.Configuration.
                var records = csv.GetRecords<MySecretPassword>().ToList();
               

                if (ModelState.IsValid) 
                {

                    _context.AddRange(records);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Perfect!! Import succesful!!";


                }

                

                return View();


            }

            catch(Exception ex)
            {

               
                ViewBag.Message = "Oops something went wrong.Error is :" + ex.ToString();

            }   
            






            return View();

        }



        // GET: MySecretPasswords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySecretPassword = await _context.MySecretPasswords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mySecretPassword == null)
            {
                return NotFound();
            }

            return View(mySecretPassword);
        }

        // GET: MySecretPasswords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MySecretPasswords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,URL,Username,Passphrase")] MySecretPassword mySecretPassword)
        {
            if (ModelState.IsValid)
            {
                //HASH PASSWORD
                //var hashedPass = MyPassMask.HashMyPass(mySecretPassword.Passphrase);
                //mySecretPassword.Passphrase = hashedPass;

                _context.Add(mySecretPassword);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mySecretPassword);
        }

        // GET: MySecretPasswords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySecretPassword = await _context.MySecretPasswords.FindAsync(id);
            if (mySecretPassword == null)
            {
                return NotFound();
            }
            return View(mySecretPassword);
        }

        // POST: MySecretPasswords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,URL,Username,Passphrase")] MySecretPassword mySecretPassword)
        {
            if (id != mySecretPassword.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mySecretPassword);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MySecretPasswordExists(mySecretPassword.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mySecretPassword);
        }

        // GET: MySecretPasswords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySecretPassword = await _context.MySecretPasswords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mySecretPassword == null)
            {
                return NotFound();
            }

            return View(mySecretPassword);
        }

        // POST: MySecretPasswords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mySecretPassword = await _context.MySecretPasswords.FindAsync(id);
            _context.MySecretPasswords.Remove(mySecretPassword);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MySecretPasswordExists(int id)
        {
            return _context.MySecretPasswords.Any(e => e.Id == id);
        }
    }
}
