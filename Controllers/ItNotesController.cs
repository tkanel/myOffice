using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myITOffice.Data;
using myITOffice.Models;

namespace myITOffice.Controllers
{

    [Authorize]

    public class ItNotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItNotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItNotes
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
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


            var itNotes = from s in _context.ItNotes
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                itNotes = itNotes.Where(s => s.Descr.Contains(searchString));

            }


            switch (sortOrder)
            {
                case "Description":
                    itNotes = itNotes.OrderByDescending(s => s.Descr);
                    break;

                case "Category":
                    itNotes = itNotes.OrderByDescending(s => s.Category.Descr);
                    break;

                case "Device Type":
                    itNotes = itNotes.OrderByDescending(s => s.DeviceType.Description);
                    break;

                case "Device":
                    itNotes = itNotes.OrderByDescending(s => s.Device.Name);
                    break;

                case "User":
                    itNotes = itNotes.OrderByDescending(s => s.OfficeUser.Name);
                    break;

                case "Created":
                    itNotes = itNotes.OrderBy(s => s.CreatedOn);
                    break;

                case "Resolved":
                    itNotes = itNotes.OrderBy(s => s.ResolvedOn);
                    break;

                default:
                    itNotes = itNotes.OrderBy(s => s.Descr);
                    break;
            }

            var pagingItNotes = itNotes.Include(b => b.Category).Include(c => c.DeviceType).Include(d => d.OfficeUser).Include(e => e.Device);
            int pageSize = 60;

            // return View(await itNotes.Include(b => b.Category).Include(c => c.DeviceType).Include(d => d.OfficeUser).Include(e => e.Device).ToListAsync());
            return View(await PaginatedList<ItNote>.CreateAsync(pagingItNotes.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: ItNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itNote = await _context.ItNotes.Include(b => b.Category).Include(c => c.DeviceType).Include(d => d.OfficeUser).Include(e => e.Device)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itNote == null)
            {
                return NotFound();
            }

            return View(itNote);
        }

        // GET: ItNotes/Create
        public IActionResult Create()
        {
            ViewBag.Descr = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Descr));
            ViewBag.DeviceType = new SelectList(_context.DeviceTypes, nameof(DeviceType.Id), nameof(DeviceType.Description));
            ViewBag.OfficeUser = new SelectList(_context.OfficeUsers, nameof(OfficeUser.Id), nameof(OfficeUser.Name));
            ViewBag.Device = new SelectList(_context.Devices, nameof(Device.Id), nameof(Device.Name));

            return View();
        }

        // POST: ItNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descr,CategoryId,DeviceTypeId,OfficeUserId,DeviceId,Notes,CreatedOn,ResolvedOn")] ItNote itNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itNote);
        }

        // GET: ItNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.Descr = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Descr));
            ViewBag.DeviceType = new SelectList(_context.DeviceTypes, nameof(DeviceType.Id), nameof(DeviceType.Description));
            ViewBag.OfficeUser = new SelectList(_context.OfficeUsers, nameof(OfficeUser.Id), nameof(OfficeUser.Name));
            ViewBag.Device = new SelectList(_context.Devices, nameof(Device.Id), nameof(Device.Name));

            if (id == null)
            {
                return NotFound();
            }

            var itNote = await _context.ItNotes.FindAsync(id);
            if (itNote == null)
            {
                return NotFound();
            }
            return View(itNote);
        }

        // POST: ItNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descr,CategoryId,DeviceTypeId,OfficeUserId,DeviceId,Notes,CreatedOn,ResolvedOn")] ItNote itNote)
        {



            if (id != itNote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItNoteExists(itNote.Id))
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
            return View(itNote);
        }

        // GET: ItNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itNote = await _context.ItNotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itNote == null)
            {
                return NotFound();
            }

            return View(itNote);
        }

        // POST: ItNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itNote = await _context.ItNotes.FindAsync(id);
            _context.ItNotes.Remove(itNote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItNoteExists(int id)
        {
            return _context.ItNotes.Any(e => e.Id == id);
        }
    }
}
