using BattleBands.Data;
using BattleBands.Models.VideoViewModels;
using BattleBands.Services;
using Microsoft.AspNetCore.Mvc;

namespace BattleBands.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;
        UnitOfWork unitOfWork;
        public HomeController(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Performers() => View();

        public IActionResult Music() => View();
        
        public IActionResult Video()
        {
            var videos = unitOfWork.Videos.GetAll();
            var items = new VideoView
            {
                Video = videos
            };
            return View(items);
        }

        public IActionResult NewPerformer() => View();

        public IActionResult Error()
        {
            return View();
        }
    }
}
