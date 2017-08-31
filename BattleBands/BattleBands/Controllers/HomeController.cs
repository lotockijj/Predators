using BattleBands.Data;
using BattleBands.Models.ViewModels.VideoViewModels;
using BattleBands.Services;
using Microsoft.AspNetCore.Mvc;

namespace BattleBands.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;
        LastItemService lastitem;
        UnitOfWork unitOfWork;
        public HomeController(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
            lastitem = new LastItemService(context);
            _context = context;
        }

        public IActionResult Index()
        {
            var result = lastitem.GetLast();
            return View(result);
        }
        public IActionResult Performers() => View();

        public IActionResult Music() => View();
        
        public IActionResult Video()
        {
            var videos = unitOfWork.Videos.GetAll();
            var items = new VideoViewModel
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
