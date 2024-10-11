using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;

namespace HotelProject.Controllers
{
    public class HotelfacilitiesController : Controller
    {
        private readonly ModelContext _context;

        public HotelfacilitiesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Hotelfacilities
        public async Task<IActionResult> Index()
        {

            var modelContext = _context.Hotelfacilities.Include(h => h.Facility).Include(h => h.Hotel);
            return View(await modelContext.ToListAsync());
        }

        // GET: Hotelfacilities/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Hotelfacilities == null)
            {
                return NotFound();
            }

            var hotelfacility = await _context.Hotelfacilities
                .Include(h => h.Facility)
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.HotelFacilitiesid == id);
            if (hotelfacility == null)
            {
                return NotFound();
            }

            return View(hotelfacility);
        }

        // GET: Hotelfacilities/Create
        public IActionResult Create()
        {
            ViewBag.Hotelid = new SelectList(_context.Hotels, "Hotelid", "Hotelname");
            ViewBag.Facilities = _context.Facilities.ToList();
            return View();
        }

        // POST: Hotelfacilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal Hotelid, List<decimal> SelectedFacilities)
        {
            if (SelectedFacilities == null || !SelectedFacilities.Any())
            {
                ModelState.AddModelError("", "Please select at least one facility.");
                ViewBag.Hotelid = new SelectList(_context.Hotels, "Hotelid", "Hotelname", Hotelid);
                ViewBag.Facilities = _context.Facilities.ToList();
                return View();
            }

            foreach (var facilityId in SelectedFacilities)
            {
                var hotelfacility = new Hotelfacility
                {
                    Hotelid = Hotelid,
                    Facilityid = facilityId
                };
                _context.Hotelfacilities.Add(hotelfacility);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Hotelfacilities/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Hotelfacilities == null)
            {
                return NotFound();
            }

            var hotelfacility = await _context.Hotelfacilities.FindAsync(id);
            if (hotelfacility == null)
            {
                return NotFound();
            }
            ViewData["Facilityid"] = new SelectList(_context.Facilities, "Facilitiesid", "Facilitiesid", hotelfacility.Facilityid);
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Hotelid", hotelfacility.Hotelid);
            return View(hotelfacility);
        }

        // POST: Hotelfacilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("HotelFacilitiesid,Hotelid,Facilityid")] Hotelfacility hotelfacility)
        {
            if (id != hotelfacility.HotelFacilitiesid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelfacility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelfacilityExists(hotelfacility.HotelFacilitiesid))
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
            ViewData["Facilityid"] = new SelectList(_context.Facilities, "Facilitiesid", "Facilitiesid", hotelfacility.Facilityid);
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Hotelid", hotelfacility.Hotelid);
            return View(hotelfacility);
        }

        // GET: Hotelfacilities/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Hotelfacilities == null)
            {
                return NotFound();
            }

            var hotelfacility = await _context.Hotelfacilities
                .Include(h => h.Facility)
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.HotelFacilitiesid == id);
            if (hotelfacility == null)
            {
                return NotFound();
            }

            return View(hotelfacility);
        }

        // POST: Hotelfacilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Hotelfacilities == null)
            {
                return Problem("Entity set 'ModelContext.Hotelfacilities'  is null.");
            }
            var hotelfacility = await _context.Hotelfacilities.FindAsync(id);
            if (hotelfacility != null)
            {
                _context.Hotelfacilities.Remove(hotelfacility);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelfacilityExists(decimal id)
        {
          return (_context.Hotelfacilities?.Any(e => e.HotelFacilitiesid == id)).GetValueOrDefault();
        }
    }
}
