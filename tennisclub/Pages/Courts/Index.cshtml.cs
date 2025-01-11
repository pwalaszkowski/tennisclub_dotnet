using Microsoft.AspNetCore.Mvc.RazorPages;
using tennisclub.Models;
using tennisclub.Controllers.Data;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Court> Courts { get; set; } = new List<Court>();

    public void OnGet()
    {
        Courts = _context.Courts.ToList();
    }
}