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
    public class UsersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(ModelContext context, IWebHostEnvironment IWebHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = IWebHostEnvironment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Users.Include(u => u.Userlogins);
            return View(await modelContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Userlogins)
                .FirstOrDefaultAsync(m => m.Usersid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["Userloginsid"] = new SelectList(_context.Userlogins, "Userloginsid", "Userloginsid");
            ViewBag.Userloginsid = new SelectList(_context.Userlogins, "Userloginsid", "Username");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Usersid,Firstname,Lastname,Phone,Email,Dateofbirth,Userloginsid,ImageFile")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.ImageFile != null)
                {
                    // 1- get rootpath
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //Guid.NewGuid() ==> generate unique string
                    string fileName = Guid.NewGuid().ToString() + "" + user.ImageFile.FileName;

                    //3- path 
                    string path = Path.Combine(wwwRootPath + "/Images/Users/", fileName);

                    //4- upload image to folder images
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }

                    user.Userimage = fileName;
                }
                if (user.Dateofbirth.HasValue)
                {
                    user.Dateofbirth = user.Dateofbirth.Value.Date;
                }

                // Add user to the database
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, repopulate the dropdown for Userloginsid
            ViewData["Userloginsid"] = new SelectList(_context.Userlogins, "Userloginsid", "Userloginsid", user.Userloginsid);
            return View(user);
        }


        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Userloginsid"] = new SelectList(_context.Userlogins, "Userloginsid", "Userloginsid", user.Userloginsid);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Usersid,Firstname,Lastname,Phone,Email,Dateofbirth,Userloginsid,Userimage")] User user)
        {
            if (id != user.Usersid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Usersid))
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
            ViewData["Userloginsid"] = new SelectList(_context.Userlogins, "Userloginsid", "Userloginsid", user.Userloginsid);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Userlogins)
                .FirstOrDefaultAsync(m => m.Usersid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ModelContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(decimal id)
        {
          return (_context.Users?.Any(e => e.Usersid == id)).GetValueOrDefault();
        }
    }
}
