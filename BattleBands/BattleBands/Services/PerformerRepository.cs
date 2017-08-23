﻿using BattleBands.Data;
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

        public void Update(ApplicationPerformer item)
        {
            var tmp = _context.Performers.Find(item.PerformerId);
            tmp.PerformerCountry = item.PerformerCountry;
            tmp.PerformerDescription = item.PerformerDescription;
            tmp.PerformerEmail = item.PerformerEmail;
            tmp.PerformerGenre = item.PerformerGenre;
            tmp.PerformerIsBand = item.PerformerIsBand;
            tmp.PerformerName = item.PerformerName;
            tmp.PerformerPhone = item.PerformerPhone;

            _context.Entry(tmp).State = EntityState.Modified;
            _context.SaveChanges();
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
