using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BattleBands.Services;
using BattleBands.Data;
using BattleBands.Models.PerformerViewModels;
using Microsoft.AspNetCore.Identity;
using BattleBands.Models;
using Microsoft.AspNetCore.Hosting;
using BattleBands.Models.MusicViewModels;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BattleBands.Controllers
{
    public class MusicController : Controller
    {
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;
        UnitOfWork unitOfWork;
        IHostingEnvironment appEnvironment;
        public MusicController(ApplicationDbContext _context, UserManager<ApplicationUser> _usrMng, IHostingEnvironment _appEnvironment)
        {
            context = _context;
            userManager = _usrMng;
            appEnvironment = _appEnvironment;
            unitOfWork = new UnitOfWork(context);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index() => View(unitOfWork.Music.GetAll());

        [HttpGet]
        [Authorize]
        public JsonResult GetMusicListMobile() => Json(unitOfWork.Music.GetAll()); 

        public IActionResult AddMusic(string id) => View();

        [HttpPost]
        public async Task<IActionResult> AddMusic(AddMusicViewModel item)
        {
            if (item.Music != null)
            {
                string path = "/content/music/" + item.Music.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await item.Music.CopyToAsync(fileStream);
                }
                var file = new ApplicationMusic { Name = item.Music.FileName, Path = path, IdOwner = item.ID, UploadTime = DateTimeOffset.Now };
                unitOfWork.Music.Create(file);
                unitOfWork.Save();
            }
            return Redirect($"~/Performer/ProfilePerformer/{item.ID}");
        }

        public IActionResult GetMusic(string id) => View(new GetMusicViewModel
        {
            Music = unitOfWork.Music.GetByAuthor(id),
            ID = id
        });
        public IActionResult DeleteMusic(string id)
        {
            var item = unitOfWork.Music.Get(id);
            unitOfWork.Music.Delete(id);
            return Redirect($"~/Music/GetMusic/{item.IdOwner}");
        }
    }
}
