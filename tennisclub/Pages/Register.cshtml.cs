using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Data;
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