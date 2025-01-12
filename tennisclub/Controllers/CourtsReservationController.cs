using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tennisclub.Controllers.Data;
using tennisclub.Models;

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
            return View(reservations);
        }

        // GET: Edit reservation
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _context.CourtReservations.FindAsync(id);

            if (reservation == null || reservation.UserId != GetCurrentUserId())
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Edit reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourtReservation reservation)
        {
            if (id != reservation.ReservationId || reservation.UserId != GetCurrentUserId())
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(reservation);
        }

        // GET: Delete reservation
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.CourtReservations.FindAsync(id);

            if (reservation == null || reservation.UserId != GetCurrentUserId())
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Delete reservation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.CourtReservations.FindAsync(id);

            if (reservation == null || reservation.UserId != GetCurrentUserId())
            {
                return NotFound();
            }

            _context.CourtReservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private int GetCurrentUserId()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                return userId.Value;
            }

            throw new InvalidOperationException("User is not logged in.");
        }
    }
}
