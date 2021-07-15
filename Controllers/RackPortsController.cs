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
    public class RackPortsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RackPortsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RackPorts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RackPorts.Include(r => r.Racks);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RackPorts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rackPort = await _context.RackPorts
                .Include(r => r.Racks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rackPort == null)
            {
                return NotFound();
            }

            return View(rackPort);
        }

        // GET: RackPorts/Create
        public IActionResult Create()
        {
                    
            ViewBag.RackBrand = new SelectList(_context.Racks, nameof(Rack.Id), nameof(Rack.Brand));
           

            return View();
        }

        // POST: RackPorts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Port,Notes,RackId")] RackPort rackPort)
        {
            if (ModelState.IsValid)
            {
                var portCheck = _context.RackPorts.FirstOrDefault(x => x.Port == rackPort.Port);
               
                if (portCheck == null)
                {

                    _context.Add(rackPort);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Alert = "Η πόρτα έχει ήδη δημιουργηθεί, σβήστε την αν χρειάζεται να ξαναδημιουργηθεί";
                    ViewData["RackId"] = new SelectList(_context.Racks, "Id", "Id", rackPort.RackId);
                    ViewBag.RackBrand = new SelectList(_context.Racks, nameof(Rack.Id), nameof(Rack.Brand));
                    return View(rackPort);

                }
                
            }
            ViewData["RackId"] = new SelectList(_context.Racks, "Id", "Id", rackPort.RackId);
            ViewBag.RackBrand = new SelectList(_context.Racks, nameof(Rack.Id), nameof(Rack.Brand));
            return View(rackPort);
        }

        // GET: RackPorts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rackPort = await _context.RackPorts.FindAsync(id);
            if (rackPort == null)
            {
                return NotFound();
            }
            ViewData["RackId"] = new SelectList(_context.Racks, "Id", "Id", rackPort.RackId);
            return View(rackPort);
        }

        // POST: RackPorts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rack,Port,Notes,RackId")] RackPort rackPort)
        {
            if (id != rackPort.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rackPort);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RackPortExists(rackPort.Id))
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
            ViewData["RackId"] = new SelectList(_context.Racks, "Id", "Id", rackPort.RackId);
            return View(rackPort);
        }

        // GET: RackPorts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rackPort = await _context.RackPorts
                .Include(r => r.Racks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rackPort == null)
            {
                return NotFound();
            }

            return View(rackPort);
        }

        // POST: RackPorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rackPort = await _context.RackPorts.FindAsync(id);
            _context.RackPorts.Remove(rackPort);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RackPortExists(int id)
        {
            return _context.RackPorts.Any(e => e.Id == id);
        }
    }
}
