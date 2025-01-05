using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Controllers.Data;
using tennisclub.Models;

namespace tennisclub.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public User User { get; set; }

        public EditModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = await _context.Users.FindAsync(User.Id);

            if (existingUser == null)
            {
                return NotFound($"Unable to update. User with ID {User.Id} was not found.");
            }

            // Update fields explicitly to prevent overwriting unintended data
            existingUser.FirstName = User.FirstName;
            existingUser.LastName = User.LastName;
            existingUser.Email = User.Email;
            existingUser.PhoneNumber = User.PhoneNumber;
            existingUser.DateOfBirth = User.DateOfBirth;
            existingUser.Address = User.Address;
            existingUser.MembershipType = User.MembershipType;
            existingUser.MembershipStartDate = User.MembershipStartDate;
            existingUser.MembershipEndDate = User.MembershipEndDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while saving changes: {ex.Message}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}