using Microsoft.AspNetCore.Mvc;

namespace BattleBands.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Performers() => View();

        public IActionResult Music() => View();

        public IActionResult Video() => View();

        public IActionResult Events() => View();

        public IActionResult NewPerformer() => View();

        public IActionResult Error()
        {
            return View();
        }
    }
}
