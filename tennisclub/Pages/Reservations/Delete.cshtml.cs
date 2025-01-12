using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Controllers.Data;

namespace tennisclub.Pages.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CourtReservation CourtReservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CourtReservation = await _context.CourtReservations.FindAsync(id);

            if (CourtReservation == null || CourtReservation.UserId != GetCurrentUserId())
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var reservation = await _context.CourtReservations.FindAsync(id);

            if (reservation == null || reservation.UserId != GetCurrentUserId())
            {
                return NotFound();
            }

            _context.CourtReservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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