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
            unitOfWork.Performers.Create(item);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
