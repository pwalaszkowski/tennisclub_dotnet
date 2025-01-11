using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace tennisclub.Pages.Users
{
    public class AboutModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}