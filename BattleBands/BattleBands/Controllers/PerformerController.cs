using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BattleBands.Models;
using BattleBands.Services;
using BattleBands.Data;
//using BattleBands.Models.PerformerViewModels;
using Microsoft.AspNetCore.Authorization;
using BattleBands.Models.PerformerViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BattleBands.Controllers
{
    public class PerformerController : Controller
    {
        ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;

        UnitOfWork unitOfWork;
        //RoleManager<IdentityRole> _roleManager;
        public PerformerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: /<controller>/
        public IActionResult Index()
        {
            var users = unitOfWork.Performers.GetAll();
            return View(users);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreatePerformer() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePerformer(ApplicationPerformer item)
        {
            var usr = await GetCurrentUserAsync();
            item.UserId = usr.Id;
            //item.PerformerDescription = "zahardkozheno zahardkozhenozahardkozheno zahardkozhenozahardkozheno zahardkozhenozahardkozheno zahardkozheno";
            unitOfWork.Performers.Create(item);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult ProfilePerformer(string id)
        {
            var prf = unitOfWork.Performers.Get(id);
            return View(prf);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyPerformers()
        {
            var usr = await GetCurrentUserAsync();
            return View(unitOfWork.Performers.GetAll(usr.Id));
        }

        [Authorize]
        public IActionResult DeletePerformer(string id)
        {
            unitOfWork.Performers.Delete(id);
            unitOfWork.Save();
            if (User.IsInRole("admin")) return RedirectToAction("Index");
            else return RedirectToAction("MyPerformers");
        }

        [HttpGet]
        [Authorize]
        public IActionResult UpdatePerformer(string id) => View(unitOfWork.Performers.Get(id));


        [Authorize]
        public async Task<IActionResult> UpdatePerformer(string id, ApplicationPerformer item)
        {
            item.UserId = await GetCurrentUserId();

            unitOfWork.Performers.Update(id, item);
            unitOfWork.Save();
            if (User.IsInRole("admin")) return RedirectToAction("Index");
            else return RedirectToAction("MyPerformers");
        }

        [Authorize]
        public IActionResult SearchByName(string name)
        {
            return View(unitOfWork.Performers.SearchByName(name));
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddVideo(string id)
        {
            var perf = unitOfWork.Performers.Get(id);
            var item = new ApplicationVideo();
            item.OwnerID = perf.PerformerId;
            return View(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddVideo(ApplicationVideo item)
        {
            if (item.OwnerID == null) return RedirectToAction("Error");
            try
            {
                var reference = new Uri(item.VideoReference);
                unitOfWork.Videos.Create(item);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (UriFormatException)
            {
                return RedirectToAction("AddVideo");
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("AddVideo");
            }

        }
        [Authorize]
        [HttpGet]
        public IActionResult GetPerformerVideos(string id)
        {
            var list = unitOfWork.Videos.GetAllByAuthor(id);
            return View(list);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewPerformerVideo(string id)
        {
            var item = new GetVideoAndInfoViewModel();
            item.video = unitOfWork.Videos.Get(id);
            item.reference = new Uri(item.video.VideoReference, UriKind.Absolute);
            return View(item);
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditVideo(string id) => View(unitOfWork.Videos.Get(id));

        [Authorize]
        [HttpPost]
        public IActionResult EditVideo(ApplicationVideo video)
        {
            try
            {
                unitOfWork.Videos.Update(video);
                unitOfWork.Save();
                return RedirectToAction("MyPerformers");
            }
            catch
            {
                return Redirect("Index"); //треба нормальні редіректи
            }
        }
        [Authorize]
        public IActionResult DeleteVideo(string id)
        {
            unitOfWork.Videos.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("MyPerformers");
        }
    }
}
