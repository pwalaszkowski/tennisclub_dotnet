using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tennisclub.Models;
using tennisclub.Controllers.Data;

namespace tennisclub.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CourtReservation CourtReservation { get; set; }

        public SelectList UsersSelectList { get; set; }
        public SelectList CourtsSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Load available users and courts for dropdowns
            UsersSelectList = new SelectList(await _context.Users.ToListAsync(), "Id", "FirstName");
            CourtsSelectList = new SelectList(await _context.Courts.ToListAsync(), "CourtId", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdown lists if validation fails
                UsersSelectList = new SelectList(await _context.Users.ToListAsync(), "Id", "FirstName");
                CourtsSelectList = new SelectList(await _context.Courts.ToListAsync(), "CourtId", "Name");

                return Page();
            }

            // Add the new reservation to the database
            _context.CourtReservations.Add(CourtReservation);
            await _context.SaveChangesAsync();

            // Redirect to the Index page
            return RedirectToPage("./Index");
        }
    }
}
