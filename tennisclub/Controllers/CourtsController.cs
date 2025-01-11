using Microsoft.AspNetCore.Mvc;
using tennisclub.Models;
using tennisclub.Controllers.Data;

namespace tennisclub.Controllers.Data
{
    public class CourtsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourtsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courts
        public IActionResult Index()
        {
            var courts = _context.Courts.ToList();
            return View(courts);
        }

        // GET: Courts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Court court)
        {
            if (ModelState.IsValid)
            {
                _context.Courts.Add(court);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // GET: Courts/Edit/{id}
        public IActionResult Edit(int id)
        {
            var court = _context.Courts.Find(id);
            if (court == null)
            {
                return NotFound();
            }
            return View(court);
        }

        // POST: Courts/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Court court)
        {
            if (id != court.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Courts.Update(court);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // POST: Courts/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var court = _context.Courts.Find(id);
            if (court != null)
            {
                _context.Courts.Remove(court);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}