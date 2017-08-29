﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BattleBands.Models.ApplicationModels;
using BattleBands.Models.ViewModels.EventViewModels;
using BattleBands.Services;
using BattleBands.Data;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace BattleBands.Controllers
{
    
    public class EventController : Controller
    {
        ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        UnitOfWork unitOfWork;
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
        [HttpGet]
        public IActionResult Index()
        {
            var events = unitOfWork.Events.GetAll();
            var logo = new ApplicationPhoto();

            var result = new List<EventPageViewModel>();
            foreach (var tmp in events)
            {
                logo = unitOfWork.Picture.GetLastByOwner(tmp.EventId);
                result.Add(
                    new EventPageViewModel
                    {
                        Event = tmp,
                        Logo = logo,
                    }
                );
            }
            return View(result);
        }

        [Authorize]
        public IActionResult CreateEvent() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(ApplicationEvent item)
        {
            var usr = await GetCurrentUserAsync();
            item.E_UserId = usr.Id;
            unitOfWork.Events.Create(item);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult EventPage(string id)
        {
            var result = new EventPageViewModel
            {
                Event = unitOfWork.Events.Get(id),
                Logo = unitOfWork.Picture.GetLastByOwner(id)
            };

            return View(result);
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
            item.EventId = id;
            unitOfWork.Events.Update(item);
            unitOfWork.Save();
            if (User.IsInRole("admin")) return RedirectToAction("MyEvents");
            else return RedirectToAction("Index");
        }
    }
}
