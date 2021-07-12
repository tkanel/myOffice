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
    public class LineTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LineTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LineTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LineTypes.OrderBy(s => s.Description).ToListAsync());
        }

        // GET: LineTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineType = await _context.LineTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lineType == null)
            {
                return NotFound();
            }

            return View(lineType);
        }

        // GET: LineTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LineTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Notes")] LineType lineType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lineType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lineType);
        }

        // GET: LineTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineType = await _context.LineTypes.FindAsync(id);
            if (lineType == null)
            {
                return NotFound();
            }
            return View(lineType);
        }

        // POST: LineTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Notes")] LineType lineType)
        {
            if (id != lineType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineTypeExists(lineType.Id))
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
            return View(lineType);
        }

        // GET: LineTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineType = await _context.LineTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lineType == null)
            {
                return NotFound();
            }

            return View(lineType);
        }

        // POST: LineTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lineType = await _context.LineTypes.FindAsync(id);
            _context.LineTypes.Remove(lineType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineTypeExists(int id)
        {
            return _context.LineTypes.Any(e => e.Id == id);
        }
    }
}
