using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myITOffice.Data;
using myITOffice.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using myITOffice.ViewModels;

namespace myITOffice.Controllers
{
    [Authorize]
    public class HelpDesksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HelpDesksController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: HelpDesks
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "AttachedFiles");
            ViewBag.AttachmentPath = uploadsFolder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DescrSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Description" : "";
            ViewData["CategorySortParm"] = String.IsNullOrEmpty(sortOrder) ? "Category" : "";
            ViewData["DeviceTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Device Type" : "";
            ViewData["DeviceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Device" : "";
            ViewData["UserSortParm"] = String.IsNullOrEmpty(sortOrder) ? "User" : "";
            ViewData["CreatedSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Created" : "";
            ViewData["ResolvedSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Resolved" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }



            ViewData["CurrentFilter"] = searchString;


            var helpDesks = from s in _context.HelpDesks
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                helpDesks = helpDesks.Where(s => s.Descr.Contains(searchString));

            }


            switch (sortOrder)
            {
                case "Description":
                    helpDesks = helpDesks.OrderByDescending(s => s.Descr);
                    break;

                case "Category":
                    helpDesks = helpDesks.OrderByDescending(s => s.Category.Descr);
                    break;

                case "Device Type":
                    helpDesks = helpDesks.OrderByDescending(s => s.DeviceType.Description);
                    break;

                case "Device":
                    helpDesks = helpDesks.OrderByDescending(s => s.Device.Name);
                    break;

                case "User":
                    helpDesks = helpDesks.OrderByDescending(s => s.OfficeUser.Name);
                    break;

                case "Created":
                    helpDesks = helpDesks.OrderBy(s => s.CreatedOn);
                    break;

                case "Resolved":
                    helpDesks = helpDesks.OrderBy(s => s.ResolvedOn);
                    break;

                default:
                    helpDesks = helpDesks.OrderBy(s => s.ResolvedOn).ThenByDescending(s => s.CreatedOn);
                    break;
            }

            var pagingHelpDesks = helpDesks.Include(b => b.Category).Include(c => c.DeviceType).Include(d => d.OfficeUser).Include(e => e.Device);
            int pageSize = 60;



            //var applicationDbContext = _context.HelpDesks.Include(h => h.Category).Include(h => h.Device).Include(h => h.DeviceType).Include(h => h.OfficeUser);
            return View(await PaginatedList<HelpDesk>.CreateAsync(pagingHelpDesks.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: HelpDesks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpDesk = await _context.HelpDesks
                .Include(h => h.Category)
                .Include(h => h.Device)
                .Include(h => h.DeviceType)
                .Include(h => h.OfficeUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpDesk == null)
            {
                return NotFound();
            }

            return View(helpDesk);
        }







        // GET: HelpDesks/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Descr");
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Name");
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "Id", "Description");
            ViewData["OfficeUserId"] = new SelectList(_context.OfficeUsers, "Id", "Name");
            return View();
        }







        // POST: HelpDesks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HelpDeskViewModel helpDesk)
        {
            string uniqueFileName = null;

            string fileNameOriginal = null;

           // var fileLength = helpDesk.HelpDeskAttachement.Length;

            if (helpDesk.HelpDeskAttachement != null)
            {
                //copy attachement to ~/AttachedFiles
                fileNameOriginal = helpDesk.HelpDeskAttachement.FileName.ToString();
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "AttachedFiles");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileNameOriginal;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                helpDesk.HelpDeskAttachement.CopyTo(new FileStream(filePath, FileMode.Create));

            }




            if (ModelState.IsValid)
            {


                HelpDesk newHelpDesk = new HelpDesk()
                {

                    Descr = helpDesk.Descr,
                    CreatedOn = helpDesk.CreatedOn,
                    ResolvedOn = helpDesk.ResolvedOn,
                    Notes = helpDesk.Notes,
                    HelpDeskAttachement = uniqueFileName,
                    CategoryId = helpDesk.CategoryId,
                    DeviceTypeId = helpDesk.DeviceTypeId,
                    OfficeUserId = helpDesk.OfficeUserId,
                    DeviceId = helpDesk.DeviceId




                };

                _context.Add(newHelpDesk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Descr", helpDesk.CategoryId);
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Name", helpDesk.DeviceId);
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "Id", "Description", helpDesk.DeviceTypeId);
            ViewData["OfficeUserId"] = new SelectList(_context.OfficeUsers, "Id", "Name", helpDesk.OfficeUserId);
            return View(helpDesk);
        }








        // GET: HelpDesks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpDesk = await _context.HelpDesks.FindAsync(id);
            if (helpDesk == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Descr", helpDesk.CategoryId);
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Name", helpDesk.DeviceId);
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "Id", "Description", helpDesk.DeviceTypeId);
            ViewData["OfficeUserId"] = new SelectList(_context.OfficeUsers, "Id", "Name", helpDesk.OfficeUserId);
            return View(helpDesk);
        }








        // POST: HelpDesks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descr,CreatedOn,ResolvedOn,Notes,CategoryId,DeviceTypeId,OfficeUserId,DeviceId,HelpDeskAttachement")] HelpDesk helpDesk)
        {
            if (id != helpDesk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helpDesk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelpDeskExists(helpDesk.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Descr", helpDesk.CategoryId);
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Name", helpDesk.DeviceId);
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "Id", "Description", helpDesk.DeviceTypeId);
            ViewData["OfficeUserId"] = new SelectList(_context.OfficeUsers, "Id", "Name", helpDesk.OfficeUserId);
            return View(helpDesk);
        }










        // GET: HelpDesks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpDesk = await _context.HelpDesks
                .Include(h => h.Category)
                .Include(h => h.Device)
                .Include(h => h.DeviceType)
                .Include(h => h.OfficeUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpDesk == null)
            {
                return NotFound();
            }

            return View(helpDesk);
        }








        // POST: HelpDesks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var helpDesk = await _context.HelpDesks.FindAsync(id);
            _context.HelpDesks.Remove(helpDesk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelpDeskExists(int id)
        {
            return _context.HelpDesks.Any(e => e.Id == id);
        }
    }
}
