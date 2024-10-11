using HotelProject.Interfaces;
using HotelProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Net.Mail;
using iText.Bouncycastleconnector;
using iText.Layout.Borders;
using iText.Layout.Properties;
using System.Net;
using iText.Commons.Actions.Contexts;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas.Draw;
namespace HotelProject.Controllersl

{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;



        public HomeController(ILogger<HomeController> logger, ModelContext context,
            IWebHostEnvironment webHostEnviroment, IEmailSender emailSender)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnviroment;
            _emailSender = emailSender;
        }
        
       
        public IActionResult Index()
        {
            var Comments = _context.Testimonials.Include(x => x.Users).Where(x => x.Status == "Approved").ToList();
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2;
            ViewBag.Address = HeaderFooterData.Address;
            ViewBag.PhoneNumber = HeaderFooterData.PhoneNumber;
            ViewBag.Email = HeaderFooterData.Email;
            ViewBag.Facebook = HeaderFooterData.Facebook;
            ViewBag.Instagram = HeaderFooterData.Instagram;
            ViewBag.Youtube = HeaderFooterData.Youtube;
            ViewBag.Twitter = HeaderFooterData.Twitter;
            ViewBag.CopyRightStatment = HeaderFooterData.CopyrightStatement;

            return View(Comments);
        }
        
        [HttpGet]
        public IActionResult AddTestimonial()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTestimonial([Bind("Contents,Rating")] Testimonial testimonial)
        {
            var userID = (decimal)HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid)
            {
                testimonial.Usersid = userID;
                testimonial.Testimonialdate = DateTime.Now;
                testimonial.Status = "Pending";
                _context.Testimonials.Add(testimonial);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(testimonial);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult Hotels()
        {
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2; ViewBag.Address = HeaderFooterData.Address;
            ViewBag.PhoneNumber = HeaderFooterData.PhoneNumber;
            ViewBag.Email = HeaderFooterData.Email;
            ViewBag.Facebook = HeaderFooterData.Facebook;
            ViewBag.Instagram = HeaderFooterData.Instagram;
            ViewBag.Youtube = HeaderFooterData.Youtube;
            ViewBag.Twitter = HeaderFooterData.Twitter;
            ViewBag.CopyRightStatment = HeaderFooterData.CopyrightStatement;
            var Citys = _context.Cities.ToList();
            return View(Citys);

        }

        public IActionResult GetHotelByCityID(int id)
        {
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2;
            ViewBag.Address = HeaderFooterData.Address;
            ViewBag.PhoneNumber = HeaderFooterData.PhoneNumber;
            ViewBag.Email = HeaderFooterData.Email;
            ViewBag.Facebook = HeaderFooterData.Facebook;
            ViewBag.Instagram = HeaderFooterData.Instagram;
            ViewBag.Youtube = HeaderFooterData.Youtube;
            ViewBag.Twitter = HeaderFooterData.Twitter;
            ViewBag.CopyRightStatment = HeaderFooterData.CopyrightStatement;

            var Hotels = _context.Hotels
                .Where(x => x.Cityid == (decimal)id)
                .Include(h => h.Hotelfacilities)  // Include hotel facilities
                .ThenInclude(hf => hf.Facility)
                .ToList();

            return View(Hotels);
        }

        [HttpGet]
        public IActionResult GetRooms(int id)
        {
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2; ViewBag.Address = HeaderFooterData.Address;
            ViewBag.PhoneNumber = HeaderFooterData.PhoneNumber;
            ViewBag.Email = HeaderFooterData.Email;
            ViewBag.Facebook = HeaderFooterData.Facebook;
            ViewBag.Instagram = HeaderFooterData.Instagram;
            ViewBag.Youtube = HeaderFooterData.Youtube;
            ViewBag.Twitter = HeaderFooterData.Twitter;
            ViewBag.CopyRightStatment = HeaderFooterData.CopyrightStatement;
            var rooms = _context.Rooms.Where(x => x.Hotelid == id).ToList();
            return View(rooms);

        }

        public IActionResult SetReservation(int roomId)
        {
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2; ViewBag.Address = HeaderFooterData.Address;
            ViewBag.PhoneNumber = HeaderFooterData.PhoneNumber;
            ViewBag.Email = HeaderFooterData.Email;
            ViewBag.Facebook = HeaderFooterData.Facebook;
            ViewBag.Instagram = HeaderFooterData.Instagram;
            ViewBag.Youtube = HeaderFooterData.Youtube;
            ViewBag.Twitter = HeaderFooterData.Twitter;
            ViewBag.CopyRightStatment = HeaderFooterData.CopyrightStatement;
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "LoginsAndRegister");
            }
            var room = _context.Rooms.FirstOrDefault(r => r.Roomid == roomId);

            if (room == null)
            {
                return NotFound();
            }

            var model = new Reservation
            {
                Roomid = room.Roomid,
                // Set other room-related info
                Totalprice = room.Pricepernight // Pass the price per night to the model
            };

            return View(model); // Pass the reservation model to the view
        }

        [HttpPost]
        public async Task<IActionResult> SetReservation([Bind("Roomid,Checkindate,Checkoutdate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var room = await _context.Rooms.FindAsync(reservation.Roomid);

                if (room == null)
                {
                    return NotFound();
                }

                var nights = (reservation.Checkoutdate - reservation.Checkindate).Days;

                if (nights <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Checkout date must be after check-in date.");
                    return View(reservation);
                }

                var totalPrice = nights * room.Pricepernight ?? 0;

                // Store all data as strings in TempData
                TempData["RoomId"] = reservation.Roomid.ToString(); // Convert RoomId to string
                TempData["CheckinDate"] = reservation.Checkindate.ToString("yyyy-MM-dd"); // Convert Checkindate to string
                TempData["CheckoutDate"] = reservation.Checkoutdate.ToString("yyyy-MM-dd"); // Convert Checkoutdate to string
                TempData["TotalPrice"] = totalPrice.ToString(); // Convert decimal to string
                TempData["ReservationDate"] = reservation.Reservationdate.ToString("yyyy-MM-dd");
                

                return RedirectToAction("Payment");
            }

            return View(reservation);
        }

        public IActionResult Payment()
        {
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2; ViewBag.Address = HeaderFooterData.Address;
            ViewBag.PhoneNumber = HeaderFooterData.PhoneNumber;
            ViewBag.Email = HeaderFooterData.Email;
            ViewBag.Facebook = HeaderFooterData.Facebook;
            ViewBag.Instagram = HeaderFooterData.Instagram;
            ViewBag.Youtube = HeaderFooterData.Youtube;
            ViewBag.Twitter = HeaderFooterData.Twitter;
            ViewBag.CopyRightStatment = HeaderFooterData.CopyrightStatement;
            if (TempData["RoomId"] == null)
            {
                return RedirectToAction("GetRooms");
            }

            // Retrieve and parse TempData values
            ViewBag.RoomId = int.Parse(TempData["RoomId"].ToString());
            ViewBag.CheckinDate = DateTime.Parse(TempData["CheckinDate"].ToString());
            ViewBag.CheckoutDate = DateTime.Parse(TempData["CheckoutDate"].ToString());
            ViewBag.TotalPrice = decimal.Parse(TempData["TotalPrice"].ToString());

            // Keep TempData values so they can be used in subsequent requests
            TempData.Keep("RoomId");
            TempData.Keep("CheckinDate");
            TempData.Keep("CheckoutDate");
            TempData.Keep("TotalPrice");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
public async Task<IActionResult> Payment([Bind("Cardnumber,Expirydate,Cvv,Balance")] Bank bank)
    {
        var userSession = HttpContext.Session.GetInt32("UserId");
        var myUser = await _context.Users.Where(x => x.Usersid == (decimal)userSession).FirstOrDefaultAsync();

        var totalPrice = TempData["TotalPrice"] != null ? decimal.Parse(TempData["TotalPrice"].ToString()) : 0;
        var checkInDate = TempData["CheckinDate"] != null ? DateTime.Parse(TempData["CheckinDate"].ToString()) : DateTime.MinValue;
        var checkOutDate = TempData["CheckoutDate"] != null ? DateTime.Parse(TempData["CheckoutDate"].ToString()) : DateTime.MinValue;
        var reservationDate = TempData["ReservationDate"] != null ? DateTime.Parse(TempData["ReservationDate"].ToString()) : DateTime.MinValue;
        var roomId = TempData["RoomId"] != null ? int.Parse(TempData["RoomId"].ToString()) : 0;

        var user = _context.Banks
            .Where(u => u.Cardnumber == bank.Cardnumber &&
                        u.Expirydate == bank.Expirydate &&
                        u.Cvv == bank.Cvv)
            .FirstOrDefault();

        string subject = $"Your invoice for a reservation with Name: {myUser.Firstname} {myUser.Lastname}";
        string content = $"Invoice Details\n\n" +
                         $"Name: {myUser.Firstname} {myUser.Lastname}\n" +
                         $"Total Price: {totalPrice:C}\n" +
                         $"Check-In Date: {checkInDate:yyyy-MM-dd}\n" +
                         $"Check-Out Date: {checkOutDate:yyyy-MM-dd}\n" +
                         $"Thank you for your reservation!";

        if (user != null)
        {
            if (user.Balance >= totalPrice)
            {
                user.Balance -= totalPrice;

                // Generate PDF Invoice with enhancements
                byte[] pdfBytes;
                using (var memoryStream = new MemoryStream())
                {
                    var writer = new PdfWriter(memoryStream);
                    var pdf = new PdfDocument(writer);
                    var document = new Document(pdf);

                    // Add header
                    document.Add(new Paragraph("Invoice")
                        .SetFontSize(20)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new LineSeparator(new SolidLine()));

                    // Create a table for invoice details
                    Table table = new Table(2);
                    table.SetWidth(UnitValue.CreatePercentValue(100));

                    // Add table headers
                    table.AddCell(new Cell().Add(new Paragraph("Description").SetBold()).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                    table.AddCell(new Cell().Add(new Paragraph("Details").SetBold()).SetBackgroundColor(ColorConstants.LIGHT_GRAY));

                    // Add table data
                    table.AddCell(new Cell().Add(new Paragraph("Name:")));
                    table.AddCell(new Cell().Add(new Paragraph($"{myUser.Firstname} {myUser.Lastname}")));

                    table.AddCell(new Cell().Add(new Paragraph("Total Price:")));
                    table.AddCell(new Cell().Add(new Paragraph($"{totalPrice:C}")));

                    table.AddCell(new Cell().Add(new Paragraph("Check-In Date:")));
                    table.AddCell(new Cell().Add(new Paragraph($"{checkInDate:yyyy-MM-dd}")));

                    table.AddCell(new Cell().Add(new Paragraph("Check-Out Date:")));
                    table.AddCell(new Cell().Add(new Paragraph($"{checkOutDate:yyyy-MM-dd}")));

                    document.Add(table);

                    // Add footer
                    document.Add(new Paragraph("\nThank you for your reservation!")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(12));

                    document.Close();
                    pdfBytes = memoryStream.ToArray();
                }

                // Create the PDF attachment
                Attachment pdfAttachment = new Attachment(new MemoryStream(pdfBytes), "Invoice.pdf", "application/pdf");

                // Send Email with PDF attachment
                await _emailSender.SendEmailWithAttachmentAsync(myUser.Email, subject, content, pdfAttachment);

                // Create a new Reservation
                var newReservation = new Reservation
                {
                    Reservationdate = reservationDate,
                    Checkindate = checkInDate,
                    Checkoutdate = checkOutDate,
                    Totalprice = totalPrice,
                    Userid = myUser.Usersid,
                    Roomid = roomId
                };

                _context.Reservations.Add(newReservation);
                _context.Banks.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("PaymentSuccessfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Insufficient balance.");
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid card details.");
        }

        return View(bank);
    }

   
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        // Method to generate PDF Invoice (Example)
    /*        private byte[] GeneratePdfInvoice(User myUser, decimal totalPrice, DateTime checkInDate, DateTime checkOutDate)
            {
                // Implement your PDF generation logic here and return the byte array of the PDF.
                // For simplicity, this is an example. Use libraries like iTextSharp or PdfSharp.
                return new byte[0]; // Replace with actual PDF byte array.
            }
    */
    // Email sending function with attachment
    /*        public Task SendEmailWithAttachmentAsync(string email, string subject, string message, Attachment attachment)
            {
                var mail = "Ta7alf@hotmail.com";
                var pw = "wassem48";

                var client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, pw)
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(email);
                mailMessage.Attachments.Add(attachment);

                return client.SendMailAsync(mailMessage);
            }
    */
    public IActionResult PaymentSuccessfully()
        {
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2; ViewBag.Address = HeaderFooterData.Address;
            ViewBag.PhoneNumber = HeaderFooterData.PhoneNumber;
            ViewBag.Email = HeaderFooterData.Email;
            ViewBag.Facebook = HeaderFooterData.Facebook;
            ViewBag.Instagram = HeaderFooterData.Instagram;
            ViewBag.Youtube = HeaderFooterData.Youtube;
            ViewBag.Twitter = HeaderFooterData.Twitter;
            ViewBag.CopyRightStatment = HeaderFooterData.CopyrightStatement;
            return View();
        }
        public IActionResult MyProfile()
        {
            // Assume user is authenticated and UserId is stored in session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users
                .Include(u => u.Userlogins)
                .FirstOrDefault(u => u.Usersid == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> MyProfile([Bind("Usersid,Firstname,Lastname,Dateofbirth,Phone,Email,ImageFile")] User user, string username, string PasswordHash)
        {
            user.Usersid = (decimal)HttpContext.Session.GetInt32("UserId");

            var existingUser = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Usersid == user.Usersid);

            if (existingUser == null)
            {
                return NotFound();
            }

            ModelState.Remove("ImageFile");
            ModelState.Remove("PasswordHash");

            if (ModelState.IsValid)
            {
                // Retrieve the current user login info based on the stored Userloginsid
                var existingUserlogin = await _context.Userlogins
                    .FirstOrDefaultAsync(u => u.Userloginsid == existingUser.Userloginsid);

                if (existingUserlogin == null)
                {
                    return NotFound("User login not found.");
                }

                // Update username if provided, else keep the existing username
                if (!string.IsNullOrEmpty(username))
                {
                    existingUserlogin.Username = username;
                }

                // Update password if provided
                if (!string.IsNullOrEmpty(PasswordHash))
                {
                    existingUserlogin.PasswordHash = PasswordHash;
                }

                // Handle image file upload only if a new file is provided
                if (user.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }

                    // Update the user image with the new file name
                    user.Userimage = fileName;
                }
                else
                {
                    // Keep the existing image if no new file is uploaded
                    user.Userimage = existingUser.Userimage;
                }

                // Update user login info and other user fields
                _context.Update(existingUserlogin);  // Save the username and password changes
                user.Userloginsid = existingUserlogin.Userloginsid;  // Ensure the user login ID is maintained
                _context.Update(user);  // Update the user profile
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // Return the view with validation errors if the ModelState is not valid
            return View(user);
        }







        public IActionResult AboutUs()
        {
            

            var AboutUsInfo = _context.AboutUs.FirstOrDefault();
            ViewBag.one = AboutUsInfo.Paragraph;
            ViewBag.two = AboutUsInfo.LeftHeader;
            ViewBag.three = AboutUsInfo.RightHeader;
            ViewBag.Image = AboutUsInfo.Image;
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2;
            return View();
        }
        public IActionResult ContactUs()
        {
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2;
            var Details = _context.ContactUs.FirstOrDefault();
            ViewBag.Address = Details.Address;
            ViewBag.PhoneNumber = Details.PhoneNumber;
            ViewBag.Email = Details.Email;
            ViewBag.Pargraph = Details.Paragraph;
            ViewBag.Iframe = Details.Iframe;
            ViewBag.Image = Details.ContactImage;

            return View();
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ContactUs(string name, string email, string message)
        {
            if (ModelState.IsValid)
            {
                var subject = $"New Contact Us Message from {name}";
                var fullMessage = $"Message from {name} ({email}): <br/><br/>{message}";

                // Use the form email as the sender and specify the fixed receiver email
                await _emailSender.SendEmailAsync(email, "wassem303@hotmail.com", subject, fullMessage);

                ViewBag.Message = "Your message has been sent successfully.";
            }
            else
            {
                ViewBag.Message = "Failed to send the message. Please try again.";
            }

            return View();
        }

        public IActionResult MyReservations()
        {
            var HeaderFooterData = _context.HeaderFooter.FirstOrDefault();
            ViewBag.LogoPart1 = HeaderFooterData.LogoPart1;
            ViewBag.LogoPart2 = HeaderFooterData.LogoPart2; ViewBag.Address = HeaderFooterData.Address;
            ViewBag.PhoneNumber = HeaderFooterData.PhoneNumber;
            ViewBag.Email = HeaderFooterData.Email;
            ViewBag.Facebook = HeaderFooterData.Facebook;
            ViewBag.Instagram = HeaderFooterData.Instagram;
            ViewBag.Youtube = HeaderFooterData.Youtube;
            ViewBag.Twitter = HeaderFooterData.Twitter;
            ViewBag.CopyRightStatment = HeaderFooterData.CopyrightStatement;
            var userSession = HttpContext.Session.GetInt32("UserId");
            if (userSession == null)
            {
                return RedirectToAction("Login", "LoginsAndRegister");
            }

            var userInfo = _context.Reservations
                .Where(r => r.Userid == userSession)
                .Include(r => r.Room)
                .ToList();

            return View(userInfo);
        }
    }
}

