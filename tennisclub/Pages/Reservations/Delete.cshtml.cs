using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Models;
using tennisclub.Controllers.Data;

namespace tennisclub.Pages.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public CourtReservation Reservation { get; set; }

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _context.CourtReservations.FindAsync(id);
            if (Reservation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Reservation = await _context.CourtReservations.FindAsync(id);

            if (Reservation != null)
            {
                _context.CourtReservations.Remove(Reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
