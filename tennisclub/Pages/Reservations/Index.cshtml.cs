using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tennisclub.Controllers.Data;

namespace tennisclub.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CourtReservation> CourtReservations { get; set; }
        public int CurrentUserId { get; private set; }

        public async Task OnGetAsync()
        {
            CurrentUserId = HttpContext.Session.GetInt32("UserId") ?? 0;

            CourtReservations = await _context.CourtReservations
                .Include(r => r.User)
                .Include(r => r.Court)
                .ToListAsync();
        }
    }
}
