using BattleBands.Data;
using BattleBands.Models.ApplicationModels;
using BattleBands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Services
{
    public class VideoRepository : IRepository<ApplicationVideo>
    {
        ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(ApplicationVideo item)
        {
            item.AddTime = DateTimeOffset.Now;
            _context.Videos.Add(item);
        }

        public void Delete(string id)
        {
            ApplicationVideo av = _context.Videos.Find(id);
            _context.Videos.Remove(av);
        }

        public ApplicationVideo Get(string id)
        {
            var av = _context.Videos.Find(id);
            return av;
        }

        public IEnumerable<ApplicationVideo> GetAllByAuthor(string id)
        {
            var AuthorVideos = new List<ApplicationVideo>();
            foreach (var item in _context.Videos.OrderByDescending(x => x.AddTime).AsEnumerable())
            {
                if (item.OwnerID == id) AuthorVideos.Add(item);
            }
            return AuthorVideos;
        }

        public IEnumerable<ApplicationVideo> GetAll()
        {
            return _context.Videos.OrderByDescending(x => x.AddTime).AsEnumerable();
        }

        public void Update(ApplicationVideo item)
        {
            var tmp = _context.Videos.Find(item.VideoId);
            tmp.VideoDescription = item.VideoDescription;
            tmp.VideoName = item.VideoName;
            tmp.VideoReference = item.VideoReference;
            tmp.OwnerID = item.OwnerID;

            _context.Entry(tmp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public ApplicationVideo GetLast()
        {
            return _context.Videos.OrderBy(x => x.AddTime).Last();
        }
    }
}
