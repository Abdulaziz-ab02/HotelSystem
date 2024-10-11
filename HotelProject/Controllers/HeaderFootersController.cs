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
    public class HeaderFootersController : Controller
    {
        private readonly ModelContext _context;

        public HeaderFootersController(ModelContext context)
        {
            _context = context;
        }

        // GET: HeaderFooters
        public async Task<IActionResult> Index()
        {
              return _context.HeaderFooter != null ? 
                          View(await _context.HeaderFooter.ToListAsync()) :
                          Problem("Entity set 'ModelContext.HeaderFooter'  is null.");
        }

        // GET: HeaderFooters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HeaderFooter == null)
            {
                return NotFound();
            }

            var headerFooter = await _context.HeaderFooter
                .FirstOrDefaultAsync(m => m.HeaderFooterID == id);
            if (headerFooter == null)
            {
                return NotFound();
            }

            return View(headerFooter);
        }

        // GET: HeaderFooters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HeaderFooters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeaderFooterID,LogoPart1,LogoPart2,Address,PhoneNumber,Email,Facebook,Instagram,Youtube,Twitter,CopyrightStatement")] HeaderFooter headerFooter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(headerFooter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(headerFooter);
        }

        // GET: HeaderFooters/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.HeaderFooter == null)
            {
                return NotFound();
            }

            var headerFooter = await _context.HeaderFooter.FindAsync(id);
            if (headerFooter == null)
            {
                return NotFound();
            }
            return View(headerFooter);
        }

        // POST: HeaderFooters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("HeaderFooterID,LogoPart1,LogoPart2,Address,PhoneNumber,Email,Facebook,Instagram,Youtube,Twitter,CopyrightStatement")] HeaderFooter headerFooter)
        {
            if (id != headerFooter.HeaderFooterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(headerFooter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeaderFooterExists((int)headerFooter.HeaderFooterID))
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
            return View(headerFooter);
        }

        // GET: HeaderFooters/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.HeaderFooter == null)
            {
                return NotFound();
            }

            var headerFooter = await _context.HeaderFooter
                .FirstOrDefaultAsync(m => m.HeaderFooterID == id);
            if (headerFooter == null)
            {
                return NotFound();
            }

            return View(headerFooter);
        }

        // POST: HeaderFooters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.HeaderFooter == null)
            {
                return Problem("Entity set 'ModelContext.HeaderFooter'  is null.");
            }
            var headerFooter = await _context.HeaderFooter.FindAsync(id);
            if (headerFooter != null)
            {
                _context.HeaderFooter.Remove(headerFooter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeaderFooterExists(int id)
        {
          return (_context.HeaderFooter?.Any(e => e.HeaderFooterID == id)).GetValueOrDefault();
        }
    }
}
