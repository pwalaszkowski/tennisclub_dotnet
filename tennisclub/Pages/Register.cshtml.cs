using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Controllers.Data;
using tennisclub.Models;
using System.Security.Cryptography;
using System.Text;

namespace tennisclub.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public User NewUser { get; set; }
        public string ErrorMessage { get; set; }

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if username or email already exists
            if (_context.Users.Any(u => u.Login == NewUser.Login))
            {
                ErrorMessage = "Username is already taken. Please choose a different username.";
                return Page();
            }

            if (_context.Users.Any(u => u.Email == NewUser.Email))
            {
                ErrorMessage = "Email is already registered. Please use a different email.";
                return Page();
            }

            // Hash the password
            using var sha256 = SHA256.Create();
            var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(NewUser.Password)));

            NewUser.Password = hashedPassword;

            _context.Users.Add(NewUser);
            _context.SaveChanges();

            return RedirectToPage("Login");
        }
    }
}