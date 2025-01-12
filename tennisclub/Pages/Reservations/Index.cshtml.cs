using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tennisclub.Controllers.Data;
using tennisclub.Models;

namespace tennisclub.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CourtReservation> CourtReservations { get; set; }

        public async Task OnGetAsync()
        {
            CourtReservations = await _context.CourtReservations
                .Include(r => r.User)
                .Include(r => r.Court)
                .ToListAsync();
        }
    }
}