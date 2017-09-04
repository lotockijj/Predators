using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BattleBands.Data;
using BattleBands.Models.ApplicationModels;
using BattleBands.Models.ViewModels;
using BattleBands.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BattleBands.Models.ViewModels.CommentViewModels;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BattleBands.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly CommentService _commentService;

        public CommentController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _unitOfWork = new UnitOfWork(context);
            _commentService = new CommentService(context);
        }
        #region [Helpers]

        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        #endregion

        public async Task<IActionResult> Index(string id)
        {
            var usr = await GetCurrentUserAsync();
            return View(new GetCommentsViewModel
            {
                CommentsList = _commentService.GetComments(id),
                Comment = new ApplicationComment
                {
                    Id = new Guid().ToString(),
                    OwnerId = usr.Id,
                    DestinationId = id
                }
            });
            
        }

        public async Task<IActionResult> MakeComment(string id)
        {
            var usr = await GetCurrentUserAsync();
            return View(new ApplicationComment
            {
                Id = null,
                OwnerId = usr.Id,
                DestinationId = id
            });
        }

        [HttpPost]
        public IActionResult MakeComment(GetCommentsViewModel item)
        {
            item.Comment.Id = null;
            _unitOfWork.Comments.Create(item.Comment);
            return Redirect($"~/Comment/Index/{item.Comment.DestinationId}");
        }

        public IActionResult UserDeleteComment(string id)
        {
            _unitOfWork.Comments.UserDeleteComment(id);
            string ret = _unitOfWork.Comments.Get(id).DestinationId;
            return Redirect($"~/Comment/Index/{ret}");
        }
        [HttpGet]
        public IActionResult EditComment(string id) => View(_unitOfWork.Comments.Get(id));

        [HttpPost]
        public IActionResult EditComment(ApplicationComment item)
        {
            _unitOfWork.Comments.Update(item);
            return Redirect($"~/Comment/Index/{item.DestinationId}");

        }

        [Authorize(Roles = "admin")]
        public IActionResult RemoveComment(string id)
        {
            string ret = _unitOfWork.Comments.Get(id).DestinationId;
            _unitOfWork.Comments.Delete(id);
            return Redirect($"~/Comment/Index/{ret}");
        }

    }
}
