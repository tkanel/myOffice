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
    public class DevicesController : Controller
    {
       

        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Devices
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DevicenameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Device Name" : "";
            ViewData["DeviceTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Device Type" : "";
            ViewData["DeviceIPSortParm"] = String.IsNullOrEmpty(sortOrder) ? "IP" : "";


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }



            ViewData["CurrentFilter"] = searchString;


            var devices = from s in _context.Devices
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                devices = devices.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString) || s.DeviceType.Description.Contains(searchString));

            }


            switch (sortOrder)
            {
                case "Device Name":
                    devices = devices.OrderByDescending(s => s.Name);
                    break;

                case "Device Type":
                    devices = devices.OrderByDescending(s => s.DeviceType.Description);
                    break;

                case "IP":
                    devices = devices.OrderBy(s => s.IPAddress1);
                    break;

                default:
                    devices = devices.OrderByDescending(s => s.Name);
                    break;
            }

            var pagingDevices = devices.Include(c => c.DeviceType).Include(d => d.OfficeUser);
            int pageSize = 60;


            // return View(await _context.Devices.Include(c => c.DeviceType).Include(d => d.OfficeUser).OrderBy(m=>m.Name).ToListAsync());
            return View(await PaginatedList<Device>.CreateAsync(pagingDevices.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewBag.DeviceType = new SelectList(_context.DeviceTypes, nameof(DeviceType.Id), nameof(DeviceType.Description));
            ViewBag.OfficeUser = new SelectList(_context.OfficeUsers, nameof(OfficeUser.Id), nameof(OfficeUser.Name));
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Notes,DeviceTypeId,OfficeUserId,IPAddress1,IPAddress2,IPAddress3,IPAddress4,IPAddress5,MAC,UserName,Password")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.DeviceType = new SelectList(_context.DeviceTypes, nameof(DeviceType.Id), nameof(DeviceType.Description));
            ViewBag.OfficeUser = new SelectList(_context.OfficeUsers, nameof(OfficeUser.Id), nameof(OfficeUser.Name));

            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Notes,DeviceTypeId,OfficeUserId,IPAddress1,IPAddress2,IPAddress3,IPAddress4,IPAddress5,MAC,UserName,Password")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
