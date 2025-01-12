using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tennisclub.Controllers.Data;

namespace tennisclub.Controllers
{
    public class CourtReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourtReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action to display all reservations
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.CourtReservations
                .Include(r => r.User)
                .Include(r => r.Court)
                .ToListAsync();
            return View(reservations); // Ensure reservations is passed correctly
        }
    }
}
