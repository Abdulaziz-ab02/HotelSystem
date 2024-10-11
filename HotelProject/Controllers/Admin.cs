using HotelProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class Admin : Controller
    {
        private readonly ModelContext _context;
        public Admin(ModelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.registeredUsers = _context.Userlogins.Where(x => x.Roleid == 21).Count();

            var hotelRoomData = _context.Hotels
                .Select(h => new HotelRoomViewModel
                {
                    HotelName = h.Hotelname,
                    AvailableRooms = h.Rooms.Count(r => r.Availability == "1"),
                    BookedRooms = h.Rooms.Count(r => r.Availability == "0")
                })
                .ToList();

            ViewBag.HotelRoomData = hotelRoomData;

            // Fetch the top hotels by reservation count
            var topHotelsData = _context.Hotels
                .Select(h => new
                {
                    HotelName = h.Hotelname,
                    ReservationCount = h.Rooms.SelectMany(r => r.Reservations).Count()
                })
                .OrderByDescending(h => h.ReservationCount)
                .ToList();

            ViewBag.TopHotelsData = topHotelsData;

            return View();
        }
        [HttpGet]
        public IActionResult Search()
        {
            var modelContext = _context.Reservations.Include(x => x.User).Include(x => x.Room).ThenInclude(x => x.Hotel).ThenInclude(x => x.City);
            return View(modelContext);

        }

        [HttpPost]
        public IActionResult Search(DateTime? startDate, DateTime? endDate)
        {
            IQueryable<Reservation> modelContext = _context.Reservations
                .Include(x => x.User)
                .Include(x => x.Room)
                .ThenInclude(x => x.Hotel)
                .ThenInclude(x => x.City);

            if (startDate == null && endDate == null)
            {
                return View(modelContext.ToList());
            }
            else if (startDate != null && endDate == null)
            {
                modelContext = modelContext.Where(x => x.Reservationdate >= startDate);
            }
            else if (startDate == null && endDate != null)
            {
                modelContext = modelContext.Where(x => x.Reservationdate <= endDate);
            }
            else if (startDate != null && endDate != null)
            {
                modelContext = modelContext.Where(x => x.Reservationdate >= startDate && x.Reservationdate <= endDate);
            }

            return View(modelContext.ToList());
        }



    }
}
