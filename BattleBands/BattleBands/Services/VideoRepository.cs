using BattleBands.Data;
using BattleBands.Models;
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
            foreach (var item in _context.Videos)
            {
                if (item.OwnerID == id) AuthorVideos.Add(item);
            }
            return AuthorVideos;
        }

        public IEnumerable<ApplicationVideo> GetAll()
        {
            return _context.Videos;
        }

        public void Update(ApplicationVideo item)
        {
            var tmp = _context.Videos.Find(item.VideoId);
            _context.Videos.Remove(_context.Videos.Find(tmp.VideoId));
            item.VideoId = null;
            _context.Videos.Add(item);
        }
    }
}
