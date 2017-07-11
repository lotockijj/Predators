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

        public void Delete(int id)
        {
            ApplicationPerformer ap = _context.Performers.Find(id);
            if (ap != null) _context.Performers.Remove(ap);
        }

        public ApplicationPerformer Get(int id)
        {
            return _context.Performers.Find(id);
        }

        public IEnumerable<ApplicationPerformer> GetAll()
        {
            return _context.Performers;
        }

        public void Update(ApplicationPerformer item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
