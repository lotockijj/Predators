using BattleBands.Data;
using BattleBands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Services
{
    public class MusicRepository : IRepository<ApplicationMusic>
    {
        ApplicationDbContext _context;

        public MusicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(ApplicationMusic item)
        {
            _context.Music.Add(item);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var del = _context.Music.Find(id);
            _context.Music.Remove(del);
            _context.SaveChanges();
        }

        public ApplicationMusic Get(string id)
        {
            return _context.Music.Find(id);
        }

        public IEnumerable<ApplicationMusic> GetAll()
        {
            return _context.Music;
        }

        public void Update(ApplicationMusic item)
        {
            var tmp = _context.Music.Find(item.Id);
            tmp.IdOwner = item.IdOwner;
            tmp.Name = item.Name;
            tmp.Path = item.Path;
            _context.Entry(tmp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public IEnumerable<ApplicationMusic> GetByAuthor(string id)
        {
            var result = new List<ApplicationMusic>();
            foreach (var item in _context.Music)
            {
                if (item.IdOwner == id) result.Add(item);
            }
            return result;
        }
    }
}
