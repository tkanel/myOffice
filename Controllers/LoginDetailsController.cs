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
    public class LoginDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoginDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoginDetails.ToListAsync());
        }

        // GET: LoginDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginDetail = await _context.LoginDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginDetail == null)
            {
                return NotFound();
            }

            return View(loginDetail);
        }

        // GET: LoginDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoginDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,UserName,Password,Notes")] LoginDetail loginDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginDetail);
        }

        // GET: LoginDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginDetail = await _context.LoginDetails.FindAsync(id);
            if (loginDetail == null)
            {
                return NotFound();
            }
            return View(loginDetail);
        }

        // POST: LoginDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,UserName,Password,Notes")] LoginDetail loginDetail)
        {
            if (id != loginDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginDetailExists(loginDetail.Id))
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
            return View(loginDetail);
        }

        // GET: LoginDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginDetail = await _context.LoginDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginDetail == null)
            {
                return NotFound();
            }

            return View(loginDetail);
        }

        // POST: LoginDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loginDetail = await _context.LoginDetails.FindAsync(id);
            _context.LoginDetails.Remove(loginDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginDetailExists(int id)
        {
            return _context.LoginDetails.Any(e => e.Id == id);
        }
    }
}
