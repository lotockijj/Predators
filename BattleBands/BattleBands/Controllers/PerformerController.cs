using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BattleBands.Models;
using BattleBands.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BattleBands.Data;
//using BattleBands.Models.PerformerViewModels;
using Microsoft.AspNetCore.Authorization;

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
            item.PerformerDescription = "zahardkozheno zahardkozhenozahardkozheno zahardkozhenozahardkozheno zahardkozhenozahardkozheno zahardkozheno";
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
            unitOfWork.Performers.Update(id,item);
            unitOfWork.Save();
            if (User.IsInRole("admin")) return RedirectToAction("Index");
            else return RedirectToAction("MyPerformers");
        }
    }
}
