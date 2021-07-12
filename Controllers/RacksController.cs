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
    public class RacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Racks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Racks.ToListAsync());
        }

        // GET: Racks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rack = await _context.Racks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }

        // GET: Racks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Racks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,AssetNr,PortsNr,Notes")] Rack rack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rack);
        }

        // GET: Racks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rack = await _context.Racks.FindAsync(id);
            if (rack == null)
            {
                return NotFound();
            }
            return View(rack);
        }

        // POST: Racks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,AssetNr,PortsNr,Notes")] Rack rack)
        {
            if (id != rack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RackExists(rack.Id))
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
            return View(rack);
        }

        // GET: Racks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rack = await _context.Racks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }

        // POST: Racks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rack = await _context.Racks.FindAsync(id);
            _context.Racks.Remove(rack);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RackExists(int id)
        {
            return _context.Racks.Any(e => e.Id == id);
        }
    }
}
