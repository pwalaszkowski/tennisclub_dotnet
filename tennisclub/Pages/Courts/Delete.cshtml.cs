using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Data;
using tennisclub.Models;

namespace tennisclub.Pages.Courts
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Court Court { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Court = await _context.Courts.FindAsync(id);

            if (Court == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Court = await _context.Courts.FindAsync(id);

            if (Court != null)
            {
                _context.Courts.Remove(Court);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
