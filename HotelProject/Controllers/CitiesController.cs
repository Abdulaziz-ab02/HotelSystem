using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;
using Microsoft.AspNetCore.Hosting;

namespace HotelProject.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ModelContext _context;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public CitiesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
              return _context.Cities != null ? 
                          View(await _context.Cities.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Cities'  is null.");
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Cityid == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cityid,Cityname,ImageFile")] City city)
        {
            if (ModelState.IsValid)
            {
                if (city.ImageFile != null)
                {
                    // 1- get rootpath
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //Guid.NewGuid() ==> generate unique string
                    string fileName = Guid.NewGuid().ToString() + "" + city.ImageFile.FileName;

                    //3- path 
                    string path = Path.Combine(wwwRootPath + "/Images/Cities/", fileName);

                    //4- upload image to folder images
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await city.ImageFile.CopyToAsync(fileStream);
                    }

                    city.Cityimage = fileName;
                }
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Cityid,Cityname,Cityimage")] City city)
        {
            if (id != city.Cityid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Cityid))
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
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Cityid == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'ModelContext.Cities'  is null.");
            }
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(decimal id)
        {
          return (_context.Cities?.Any(e => e.Cityid == id)).GetValueOrDefault();
        }
    }
}
