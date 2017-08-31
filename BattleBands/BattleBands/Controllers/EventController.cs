using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BattleBands.Models.ApplicationModels;
using BattleBands.Models.ViewModels.EventViewModels;
using BattleBands.Services;
using BattleBands.Data;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using BattleBands.Models.ViewModels.EventViewModels.Mobile;

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

        #region [Mobile]

        #region [Test Method]
        [HttpGet]
        public IActionResult GetAllEventsMobile()
        {
            var result = new List<GetAllEventTemp>();
            var evnt = unitOfWork.Events.GetAll();
            foreach (var item in evnt)
            {
                result.Add(new GetAllEventTemp
                {
                    Event = item,
                    Logo = unitOfWork.Picture.GetLastByOwner(item.EventId)
                });
            }
            return Json(result);
        }
#endregion

        [HttpGet]
        public IActionResult GetAllEvents() 
        {
            var result = new List<GetAllEventsMobileViewModel>();
            var evnt = unitOfWork.Events.GetAll();
            foreach (var item in evnt)
            {
                result.Add(new GetAllEventsMobileViewModel
                {
                    Id = item.EventId,
                    Name = item.EventName,
                    Place = item.EventPlace,
                    Time = item.EventTime,
                    LogoPath = unitOfWork.Picture.GetLastByOwner(item.EventId).Path
                });
            }
            return Json(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult EventPageMobile(string id)
        {
            var item = new EventPageViewModel
            {
                Event = unitOfWork.Events.Get(id),
                Logo = unitOfWork.Picture.GetLastByOwner(id)
            };
            return Json(item);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEventMobile([FromBody] ApplicationEvent item)
        {
            if (item != null)
            {
                var usr = await GetCurrentUserAsync();
                item.E_UserId = usr.Id;
                unitOfWork.Events.Create(item);
                unitOfWork.Save();
                return Ok();
            }
            else return BadRequest("wrong data");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyEventsMobile()
        {
            var usr = await GetCurrentUserAsync();
            return Json(unitOfWork.Events.GetAll(usr.Id));
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteEventMobile(string id)
        {
            if (id != null)
            {
                unitOfWork.Events.Delete(id);
                unitOfWork.Save();
                return Ok();
            }
            else return BadRequest("Null ID");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateEventMobile(string id, [FromBody] ApplicationEvent item)
        {
            if (id != null && item != null)
            {
                item.E_UserId = await GetCurrentUserId();
                item.EventId = id;
                unitOfWork.Events.Update(item);
                unitOfWork.Save();
                return Ok();
            }
            else return BadRequest("bad data");
        }

        #endregion
    }
}
