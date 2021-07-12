using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myITOffice.Data;
using myITOffice.Models;

namespace myITOffice.Controllers
{
    public class TelephoneLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TelephoneLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TelephoneLines
        public async Task<IActionResult> Index(string searchString, string currentFilter, string sortOrder , int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;
            
            ViewData["NumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Number" : "";
            ViewData["SimSortParm"] = String.IsNullOrEmpty(sortOrder) ? "SIM" : "";
            ViewData["ShortCutSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Shortcut" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var telephoneLines = from s in _context.TelephoneLines.Include(t => t.LineType)
                                 select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                telephoneLines = telephoneLines.Where(s => s.Number.Contains(searchString) || s.SIM.Contains(searchString) || s.Shortcut.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "Number":
                    telephoneLines = telephoneLines.OrderByDescending(s => s.Number);
                    break;

                case "SIM":
                    telephoneLines = telephoneLines.OrderByDescending(s => s.SIM);
                    break;

                case "Shortcut":
                    telephoneLines = telephoneLines.OrderByDescending(s => s.Shortcut);
                    break;

                default:
                    telephoneLines = telephoneLines.OrderBy(s => s.Number);
                    break;
            }




            int pageSize = 100;
            int count = telephoneLines.Count();
            ViewBag.countRecords = count;

           
            return View(await PaginatedList<TelephoneLine>.CreateAsync(telephoneLines.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: TelephoneLines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telephoneLine = await _context.TelephoneLines
                .Include(t => t.LineType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telephoneLine == null)
            {
                return NotFound();
            }

            return View(telephoneLine);
        }

        // GET: TelephoneLines/Create
        public IActionResult Create()
        {
            ViewData["LineTypeId"] = new SelectList(_context.LineTypes, "Id", "Description");
            return View();
        }

        // POST: TelephoneLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,DateStart,DateExpired,SIM,PIN1,PIN2,PUK1,PUK2,Shortcut,Notes,LineTypeId")] TelephoneLine telephoneLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telephoneLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineTypeId"] = new SelectList(_context.LineTypes, "Id", "Description", telephoneLine.LineTypeId);
            return View(telephoneLine);
        }

        // GET: TelephoneLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telephoneLine = await _context.TelephoneLines.FindAsync(id);
            if (telephoneLine == null)
            {
                return NotFound();
            }
            ViewData["LineTypeId"] = new SelectList(_context.LineTypes, "Id", "Description", telephoneLine.LineTypeId);
            return View(telephoneLine);
        }

        // POST: TelephoneLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,DateStart,DateExpired,SIM,PIN1,PIN2,PUK1,PUK2,Shortcut,Notes,LineTypeId")] TelephoneLine telephoneLine)
        {
            if (id != telephoneLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telephoneLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelephoneLineExists(telephoneLine.Id))
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
            ViewData["LineTypeId"] = new SelectList(_context.LineTypes, "Id", "Description", telephoneLine.LineTypeId);
            return View(telephoneLine);
        }

        // GET: TelephoneLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telephoneLine = await _context.TelephoneLines
                .Include(t => t.LineType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telephoneLine == null)
            {
                return NotFound();
            }

            return View(telephoneLine);
        }

        // POST: TelephoneLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var telephoneLine = await _context.TelephoneLines.FindAsync(id);
            _context.TelephoneLines.Remove(telephoneLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelephoneLineExists(int id)
        {
            return _context.TelephoneLines.Any(e => e.Id == id);
        }
    }
}
