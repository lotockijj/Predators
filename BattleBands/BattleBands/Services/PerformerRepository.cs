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
        private PerformerContext db;

        public PerformerRepository(PerformerContext context)
        {
            this.db = context;
        }

        public void Create(ApplicationPerformer item)
        {
            db.Performers.Add(item);
        }

        public void Delete(int id)
        {
            ApplicationPerformer ap = db.Performers.Find(id);
            if (ap != null) db.Performers.Remove(ap);
        }

        public ApplicationPerformer Get(int id)
        {
            return db.Performers.Find(id);
        }

        public IEnumerable<ApplicationPerformer> GetAll()
        {
            return db.Performers;
        }

        public void Update(ApplicationPerformer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
