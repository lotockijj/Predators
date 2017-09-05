using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleBands.Interfaces;
using BattleBands.Models.ApplicationModels;
using BattleBands.Data;


namespace BattleBands.Services
{
    public class CommentRepository : IRepository<ApplicationComment>
    {
        private ApplicationDbContext _context;

        public CommentRepository( ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(ApplicationComment item)
        {
            item.Time = DateTimeOffset.Now;
            item.IsDeleted = item.IsEdited = false;
            item.EditTime = item.Time;
            _context.Comments.Add(item);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var item = _context.Comments.Find(id);
            _context.Comments.Remove(item);
            _context.SaveChanges();
        }

        public ApplicationComment Get(string id)
        {
            return _context.Comments.Find(id);
        }

        public IEnumerable<ApplicationComment> GetAll()
        {
            try
            {
                return _context.Comments.OrderByDescending(x => x.Time);
            }
            catch
            {
                return new List<ApplicationComment>() { new ApplicationComment() };
            }
        }

        public ApplicationComment GetLast()
        {
            try
            {
                return _context.Comments.OrderBy(x => x.Time).Last();
            }
            catch
            {
                return new ApplicationComment();
            }
        }

        public IEnumerable<ApplicationComment> GetAllByDestination(string id)
        {
            try
            {
                return _context.Comments.Where(x => x.DestinationId == id).
                    OrderByDescending(x => x.Time).
                    AsEnumerable();
            }
            catch
            {
                return new List<ApplicationComment>() { new ApplicationComment() };
            }
        }
        public IEnumerable<ApplicationComment> GetAllByAuthor(string id)
        {
            try
            {
                return _context.Comments.Where(x => x.OwnerId == id).
                    OrderByDescending(x => x.Time).
                    AsEnumerable();
            }
            catch
            {
                return new List<ApplicationComment>() { new ApplicationComment() };
            }
        }

        public void Update(ApplicationComment item)
        {
            var tmp = new ApplicationComment
            {
                Id = item.Id,
                OwnerId = item.OwnerId,
                IsDeleted = false,
                IsEdited = true,
                DestinationId = item.DestinationId,
                Time = item.Time,
                EditTime = DateTimeOffset.Now,
                Body = item.Body
            };
            _context.Entry(tmp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void UserDeleteComment(string id)
        {
            var comment = _context.Comments.Find(id);
            comment.Body = "(comment has been deleted)";
            comment.EditTime = DateTimeOffset.Now;
            comment.IsEdited = false;
            comment.IsDeleted = true;
            _context.Entry(comment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
