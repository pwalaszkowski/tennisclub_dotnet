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
            // Add logging or debugging here to check if the method is being called
            Console.WriteLine($"Received reservation request for Court {CourtReservation.CourtId} on {CourtReservation.ReservationDate}");

            // Check if the time range is valid
            if (!CourtReservation.IsValidTimeRange)
            {
                Console.WriteLine("Invalid time range");
                ModelState.AddModelError(string.Empty, "Invalid time range. Reservations must be between 7:00 and 22:00, and end time must be after start time.");
            }

            if (!ModelState.IsValid)
            {
                // Log the model state errors
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine($"Validation error: {error.ErrorMessage}");
                    }
                }

                UsersSelectList = new SelectList(await _context.Users.ToListAsync(), "Id", "FirstName");
                CourtsSelectList = new SelectList(await _context.Courts.ToListAsync(), "CourtId", "Name");
                return Page();
            }

            try
            {
                _context.CourtReservations.Add(CourtReservation);
                Console.WriteLine("Attempting to save reservation to database");
                await _context.SaveChangesAsync();
                Console.WriteLine("Reservation saved successfully");
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving reservation: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                ModelState.AddModelError(string.Empty, "An error occurred while saving the reservation.");
                UsersSelectList = new SelectList(await _context.Users.ToListAsync(), "Id", "FirstName");
                CourtsSelectList = new SelectList(await _context.Courts.ToListAsync(), "CourtId", "Name");
                return Page();
            }
        }
    }
}
