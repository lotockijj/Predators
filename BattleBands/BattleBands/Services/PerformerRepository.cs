using BattleBands.Data;
using BattleBands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BattleBands.Services
{
    public class PerformerRepository : IRepository<ApplicationPerformer>
    {
        ApplicationDbContext _context;

        public PerformerRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Create(ApplicationPerformer item)
        {
            _context.Performers.Add(item);
        }

        public void Delete(string id)
        {
            ApplicationPerformer ap = _context.Performers.Find(id);
            if (ap != null) _context.Performers.Remove(ap);
        }

        public ApplicationPerformer Get(string id)
        {
            return _context.Performers.Find(id);
        }

        public IEnumerable<ApplicationPerformer> GetAll()
        {
            return _context.Performers;
        }

        public IEnumerable<ApplicationPerformer> GetAll(string id)
        {
            var result = new List<ApplicationPerformer>();
            foreach (var perf in this._context.Performers)
            {
                if (perf.UserId == id) result.Add(perf);
            }
            return result;
        }

        public void Update(string id, ApplicationPerformer item)
        {
            var tmp = _context.Performers.Find(id);
            item.PerformerIsBand = tmp.PerformerIsBand;
            _context.Performers.Remove(_context.Performers.Find(id));
            _context.Performers.Add(item);
        }

        public void Update(ApplicationPerformer item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationPerformer> SearchByName(string name)
        {
            var result = new List<ApplicationPerformer>();
            foreach (var perf in this._context.Performers)
            {
                if (perf.PerformerName == name) result.Add(perf);
            }
            return result;
        }
    }
}
