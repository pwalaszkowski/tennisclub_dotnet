using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Controllers.Data;
using tennisclub.Models;
using System.Security.Claims;

namespace tennisclub.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<User> Users { get; private set; }
        public string CurrentUserId { get; private set; }

        public void OnGet()
        {
            // Get the logged-in user's ID as a string
            CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the list of users
            Users = _context.Users.ToList();
        }
    }
}
