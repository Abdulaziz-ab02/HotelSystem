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
    public class AboutUsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutUsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: AboutUs
        public async Task<IActionResult> Index()
        {
              return _context.AboutUs != null ? 
                          View(await _context.AboutUs.ToListAsync()) :
                          Problem("Entity set 'ModelContext.AboutUs'  is null.");
        }

        // GET: AboutUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.AboutUsID == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // GET: AboutUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AboutUsID,ImageFile,LeftHeader,RightHeader,Paragraph")] AboutUs aboutUs)
        {
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                if (aboutUs.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + aboutUs.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/PagesManagement/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await aboutUs.ImageFile.CopyToAsync(fileStream);
                    }

                    aboutUs.Image = fileName;  // Assign the uploaded file name to the user’s image field
                }
                _context.Add(aboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutUs);
        }

        // GET: AboutUs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs == null)
            {
                return NotFound();
            }
            return View(aboutUs);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("AboutUsID,ImageFile,LeftHeader,RightHeader,Paragraph")] AboutUs aboutUs)
        {
            ModelState.Remove("Image");

            if (id != aboutUs.AboutUsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutUs.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + aboutUs.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/PagesManagement/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await aboutUs.ImageFile.CopyToAsync(fileStream);
                        }

                        aboutUs.Image = fileName;  // Assign the uploaded file name to the user’s image field
                    }
                    _context.Update(aboutUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsExists(aboutUs.AboutUsID))
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
            return View(aboutUs);
        }

        // GET: AboutUs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.AboutUsID == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.AboutUs == null)
            {
                return Problem("Entity set 'ModelContext.AboutUs'  is null.");
            }
            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs != null)
            {
                _context.AboutUs.Remove(aboutUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUsExists(decimal id)
        {
          return (_context.AboutUs?.Any(e => e.AboutUsID == id)).GetValueOrDefault();
        }
    }
}
