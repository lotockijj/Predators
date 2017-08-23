using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BattleBands.Models;
using BattleBands.Services;
using BattleBands.Data;
using Microsoft.AspNetCore.Authorization;
using BattleBands.Models.PerformerViewModels;
using System.Text.RegularExpressions;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BattleBands.Controllers
{
    public class PerformerController : Controller
    {
        ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        UnitOfWork unitOfWork;
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
            unitOfWork.Performers.Create(item);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult ProfilePerformer(string id)
        {
            var item = new PerformerProfileViewModel
            {
                Performer = unitOfWork.Performers.Get(id),
                Picture = unitOfWork.Picture.GetLastByOwner(id)
            };
            return View(item);
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
            if (User.IsInRole("admin")) return RedirectToAction("Index"); //TODO: confirm removing page
            else return RedirectToAction("MyPerformers");
        }

        [HttpGet]
        [Authorize]
        public IActionResult UpdatePerformer(string id) => View(unitOfWork.Performers.Get(id));


        [Authorize]
        public async Task<IActionResult> UpdatePerformer(string id, ApplicationPerformer item)
        {
            item.UserId = await GetCurrentUserId();
            item.PerformerId = id;
            unitOfWork.Performers.Update(item);
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
            var item = new PerformerAddVideoModelView
            {
                ID = id,
                Video = new ApplicationVideo
                {
                    OwnerID = perf.PerformerId
                }
            };
            return View(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddVideo(PerformerAddVideoModelView item)
        {
            if (item.Video.OwnerID == null) return RedirectToAction("Error");
            try
            {
                item.Video.VideoReference = ExtractVideoIdFromUri(new Uri(item.Video.VideoReference));
                unitOfWork.Videos.Create(item.Video);
                unitOfWork.Save();
                return RedirectToAction("ViewPerformerVideo", new { id= item.Video.VideoId});
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
            var item = new PerformerGetVideosModelView
            {
                ID = id,
                Video = list
            };
            return View(item);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewPerformerVideo(string id)
        {
            var item = new ApplicationVideo();
            item = unitOfWork.Videos.Get(id);
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
                video.VideoReference = ExtractVideoIdFromUri(new Uri(video.VideoReference));
                unitOfWork.Videos.Update(video);
                unitOfWork.Save();
                return RedirectToAction("ViewPerformerVideo", new { id = video.VideoId });
                //return RedirectToAction("MyPerformers");
            }
            catch
            {
                try
                {
                    unitOfWork.Videos.Update(video);
                    unitOfWork.Save();
                    return RedirectToAction("ViewPerformerVideo", new { id = video.VideoId });
                }
                catch
                { 
                return Redirect("Error"); //need right redirect
                }
            }
        }
        [Authorize]
        public IActionResult DeleteVideo(string id)
        {
            var tmp = unitOfWork.Videos.Get(id);
            unitOfWork.Videos.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("GetPerformerVideos", new { id = tmp.OwnerID });
        }


        #region [Helpers]
        private const string YoutubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+";
        private static Regex regexExtractId = new Regex(YoutubeLinkRegex, RegexOptions.Compiled);
        private static string[] validAuthorities = { "youtube.com", "www.youtube.com", "youtu.be", "www.youtu.be" };

        public string ExtractVideoIdFromUri(Uri uri)
        {
            try
            {
                string authority = new UriBuilder(uri).Uri.Authority.ToLower();

                //check if the url is a youtube url
                if (validAuthorities.Contains(authority))
                {
                    //extract the id
                    var regRes = regexExtractId.Match(uri.ToString());
                    if (regRes.Success)
                    {
                        return regRes.Groups[1].Value;
                    }
                }
            }
            catch { }


            return null;
        }

        public IActionResult ConfirmPerformerDelete(string id)
        {
            var tmp = new PerformerDeleteConfirmModelView
            {
                ID = id
            };
            return View(tmp);
        }
        #endregion
    }
}
