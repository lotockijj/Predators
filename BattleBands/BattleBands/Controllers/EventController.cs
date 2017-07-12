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
    public class EventController : Controller
    {
        ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;

        UnitOfWork unitOfWork;
        //RoleManager<IdentityRole> _roleManager;
        public EventController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
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
            var events = unitOfWork.Events.GetAll();
            return View(events);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateEvent() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(ApplicationEvent item)
        {
            var usr = await GetCurrentUserAsync();
            item.E_UserId = usr.Id;
            item.EventDescription = "dhsfkjdsfhdsjkkjfhkjfhdsjkfhdsjkfhsdkjfhdskjfdshkfjdshkfjhdskfjdshkfjhdhsfkjdsfhdsjkkjfhkjfhdsjkfhdsjkfhsdkjfhdskjfdshkfjdshkfjhdskfjdshkfjh";

            unitOfWork.Events.Create(item);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult DetailEvent(string id)
        {
            var events = unitOfWork.Events.Get(id);
            return View(events);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyEvents()
        {
            var usr = await GetCurrentUserAsync();
            return View(unitOfWork.Events.GetAll(usr.Id));
        }

        [Authorize]
        public IActionResult DeleteEvent(string id)
        {
            unitOfWork.Events.Delete(id);
            unitOfWork.Save();
            if (User.IsInRole("admin")) return RedirectToAction("MyEvents");
            else return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult UpdateEvent(string id) => View(unitOfWork.Events.Get(id));


        [Authorize]
        public async Task<IActionResult> UpdateEvent(string id, ApplicationEvent item)
        {
            item.E_UserId = await GetCurrentUserId();
            unitOfWork.Events.Update(id, item);
            unitOfWork.Save();
            if (User.IsInRole("admin")) return RedirectToAction("MyEvents");
            else return RedirectToAction("Index");
        }
    }
}
