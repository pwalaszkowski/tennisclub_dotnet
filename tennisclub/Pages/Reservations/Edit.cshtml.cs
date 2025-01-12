using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Controllers.Data;

namespace tennisclub.Pages.Reservations
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingReservation = await _context.CourtReservations.FindAsync(CourtReservation.ReservationId);

            if (existingReservation == null || existingReservation.UserId != GetCurrentUserId())
            {
                return NotFound();
            }

            existingReservation.ReservationDate = CourtReservation.ReservationDate;
            existingReservation.StartTime = CourtReservation.StartTime;
            existingReservation.EndTime = CourtReservation.EndTime;
            existingReservation.Notes = CourtReservation.Notes;

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