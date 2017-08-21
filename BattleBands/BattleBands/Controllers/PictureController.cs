using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BattleBands.Data;
using Microsoft.AspNetCore.Identity;
using BattleBands.Models;
using BattleBands.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using BattleBands.Models.PictureViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BattleBands.Controllers
{
    [Authorize]
    public class PictureController : Controller
    {
        // GET: /<controller>/
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;
        UnitOfWork unitOfWork;
        IHostingEnvironment appEnvironment;
        public PictureController( ApplicationDbContext _context, UserManager<ApplicationUser> _usrMng, IHostingEnvironment _appEnvironment)
        {
            context = _context;
            userManager = _usrMng;
            appEnvironment = _appEnvironment;
            unitOfWork = new UnitOfWork(context);
        }

        public IActionResult Index() => View( unitOfWork.Picture.GetAll());

        public IActionResult AddLogo(string id) => View();

        [HttpPost]
        public async Task<IActionResult> AddLogo(AddPictureViewModel item)
        {
            if (item.Photo != null)
            {
                string path = "/content/" + item.Photo.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await item.Photo.CopyToAsync(fileStream);
                }
                var file = new ApplicationPhoto { Name = item.Photo.FileName, Path = path, IdOwner = item.ID, UploadTime = DateTimeOffset.Now };
                unitOfWork.Picture.Create(file);
                unitOfWork.Save();
            }
            return Redirect($"~/Performer/ProfilePerformer/{item.ID}");
        }
        public IActionResult AddAvatar(string id) => View();


        [HttpPost]
        public async Task<IActionResult> AddAvatar(AddPictureViewModel item)
        {
            if (item.Photo != null)
            {
                string path = "/content/" + item.Photo.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await item.Photo.CopyToAsync(fileStream);
                }
                var file = new ApplicationPhoto { Name = item.Photo.FileName, Path = path, IdOwner = item.ID, UploadTime = DateTimeOffset.Now };
                unitOfWork.Picture.Create(file);
                unitOfWork.Save();
            }
            return Redirect($"~/Manage");
        }
        //[HttpPost]
        //public async Task<IActionResult> AddAvatar(AddPictureViewModel item)
        //{
        //    await AddPicture(item);

        //    return Redirect($"/Manage");
        //}

    }
}
