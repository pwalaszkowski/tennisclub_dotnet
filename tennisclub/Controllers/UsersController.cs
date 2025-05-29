using Microsoft.AspNetCore.Mvc;

namespace tennisclub.Controllers.Data
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
