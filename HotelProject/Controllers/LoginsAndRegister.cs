using HotelProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class LoginsAndRegister : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginsAndRegister(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {

            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Usersid,Firstname,Lastname,Phone,Email,Dateofbirth,ImageFile")] User user, string UserName, string PasswordHash)
        {
            ModelState.Remove("ImageFile");
            if (ModelState.IsValid)
            {
                // Check if the username already exists in the Userlogins table
                var existingUserlogin = await _context.Userlogins
                    .FirstOrDefaultAsync(u => u.Username == UserName);
                var existingEmail = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == user.Email);
                if (existingUserlogin != null)
                {
                    ViewBag.UsernameMessage = "The username is already taken. Please choose another one.";
                    return View(user);
                }
                if (existingEmail != null)
                {
                    
                    ViewBag.UsernameMessage = "You have an account with this Email,Please sign in.";
                    return View(user);
                }

                // Create and save the Userlogin entity first
                Userlogin userlogin = new Userlogin
                {
                    Username = UserName,
                    PasswordHash = PasswordHash,  // Ensure password is hashed for security
                    Roleid = 21  // Assuming Roleid 21 is a default role
                };

                _context.Add(userlogin);
                await _context.SaveChangesAsync();

                // Now associate the User with the newly created Userlogin
                user.Userloginsid = userlogin.Userloginsid; // Assign the Userloginsid to the User

                // Handle image file upload if provided
                if (user.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/Users/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }

                    user.Userimage = fileName;  // Assign the uploaded file name to the user’s image field
                }

                // Add the User to the context and save
                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(user);  // If model state is invalid, return the view with the model for correction
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,PasswordHash")] Userlogin userlogin)
        {
            // Fetch the User entity including the related UserLogins entity
            var user = await _context.Users
                .Include(u => u.Userlogins) // Include the related UserLogins entity
                .Where(u => u.Userlogins.Username.ToLower() == userlogin.Username.ToLower() &&
                            u.Userlogins.PasswordHash == userlogin.PasswordHash)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var userLogin = user.Userlogins; // Access the UserLogins entity

                // Check the role of the user and set session variables accordingly
                if (userLogin.Roleid == 1)
                {
                    HttpContext.Session.SetInt32("UserId", (int)user.Usersid); // Set UserId in session for admin
                    HttpContext.Session.SetString("UserName", userLogin.Username); // Set Username in session
                    return RedirectToAction("Index", "Admin");
                }
                else if (userLogin.Roleid == 21)
                {
                    HttpContext.Session.SetInt32("UserId", (int)user.Usersid); // Set UserId in session for regular user
                    HttpContext.Session.SetString("UserName", userLogin.Username); // Set Username in session
                    return RedirectToAction("Index", "Home");
                }
            }

            // If user is null or no role match, return to login with error
            ModelState.AddModelError("", "Invalid username or password.");
            return View(userlogin);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  
            return RedirectToAction("Index","Home");
        }

    }
}
