using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tennisclub.Data;
using tennisclub.Models;

namespace tennisclub.Pages.Courts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Court> Courts { get; set; }

        public async Task OnGetAsync()
        {
            Courts = await _context.Courts.ToListAsync();
        }
    }
}