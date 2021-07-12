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
    public class OfficeUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfficeUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfficeUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.OfficeUsers.OrderBy(s => s.Name).ToListAsync());
        }

        // GET: OfficeUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeUser = await _context.OfficeUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officeUser == null)
            {
                return NotFound();
            }

            return View(officeUser);
        }

        // GET: OfficeUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OfficeUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InternalPhone,MobilePhone,Notes,Company")] OfficeUser officeUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(officeUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(officeUser);
        }

        // GET: OfficeUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeUser = await _context.OfficeUsers.FindAsync(id);
            if (officeUser == null)
            {
                return NotFound();
            }
            return View(officeUser);
        }

        // POST: OfficeUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InternalPhone,MobilePhone,Notes,Company")] OfficeUser officeUser)
        {
            if (id != officeUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(officeUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficeUserExists(officeUser.Id))
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
            return View(officeUser);
        }

        // GET: OfficeUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeUser = await _context.OfficeUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officeUser == null)
            {
                return NotFound();
            }

            return View(officeUser);
        }

        // POST: OfficeUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officeUser = await _context.OfficeUsers.FindAsync(id);
            _context.OfficeUsers.Remove(officeUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficeUserExists(int id)
        {
            return _context.OfficeUsers.Any(e => e.Id == id);
        }
    }
}
