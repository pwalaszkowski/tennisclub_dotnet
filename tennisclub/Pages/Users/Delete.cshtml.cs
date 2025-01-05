using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Controllers.Data;
using tennisclub.Models;

namespace tennisclub.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public User User { get; set; }

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest("User ID must be provided.");
            }

            User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound($"User with ID {id} was not found.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest("User ID must be provided.");
            }

            var userToDelete = await _context.Users.FindAsync(id);

            if (userToDelete == null)
            {
                return NotFound($"Unable to delete. User with ID {id} was not found.");
            }

            _context.Users.Remove(userToDelete);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the user: {ex.Message}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}