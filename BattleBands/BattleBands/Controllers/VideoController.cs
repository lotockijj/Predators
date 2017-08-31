using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BattleBands.Data;
using Microsoft.AspNetCore.Identity;
using BattleBands.Models.ApplicationModels;
using BattleBands.Services;
using System.Text.RegularExpressions;
using BattleBands.Models.ViewModels.VideoViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BattleBands.Controllers
{
    public class VideoController : Controller
    {
        ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        UnitOfWork unitOfWork;
        public VideoController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() => View(unitOfWork.Videos.GetAll());

        

        [HttpGet]
        [Authorize]
        public IActionResult AddVideo(string id)
        {
            var perf = unitOfWork.Performers.Get(id);
            var item = new PerformerAddVideoViewModel
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
        public IActionResult AddVideo(PerformerAddVideoViewModel item)
        {
            if (item.Video.OwnerID == null) return RedirectToAction("Error");
            try
            {
                item.Video.VideoReference = ExtractVideoIdFromUri(new Uri(item.Video.VideoReference));
                unitOfWork.Videos.Create(item.Video);
                unitOfWork.Save();
                return RedirectToAction("ViewPerformerVideo", new { id = item.Video.VideoId });
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
        public IActionResult GetVideo(string id)
        {
            var list = unitOfWork.Videos.GetAllByAuthor(id);
            var item = new PerformerGetVideosViewModel
            {
                ID = id,
                Video = list
            };
            return View(item);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewVideo(string id)
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


        #region [Mobile]

        [HttpGet]
        public IActionResult GetVideos() => Json(unitOfWork.Videos.GetAll());

        [Authorize]
        [HttpGet]
        public IActionResult GetVideoMobile(string id)
        {
            var list = unitOfWork.Videos.GetAllByAuthor(id);
            var item = new PerformerGetVideosViewModel
            {
                ID = id,
                Video = list
            };
            return Json(item);
        }

        #endregion

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
#endregion
    }
}
