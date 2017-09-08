using System;
using System.Collections.Generic;
using System.Text;
using BattleBands.Data;
using BattleBands.Models.ApplicationModels;
using BattleBands.Models.ViewModels.CommentViewModels;
using System.Linq;
using BattleBands.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BattleBands.Services
{
    public class CommentService
    {
        private ApplicationDbContext _context;
        private UnitOfWork _unitOfWork;
        public CommentService( ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<GetPerformerCommentsViewModel> GetComments(string id)
        {
            var result = new List<GetPerformerCommentsViewModel>();
            var comments = _unitOfWork.Comments.GetAllByDestination(id);
            foreach (var item in comments)
            {
                var tmp = new GetPerformerCommentsViewModel()
                {
                    Comment = item,
                    Name = _context.Users.Find(item.OwnerId).UserName
                };
                result.Add(tmp);
            }
            return result;
        }
    }
}
