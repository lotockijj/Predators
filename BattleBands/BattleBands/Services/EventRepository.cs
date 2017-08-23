using BattleBands.Data;
using BattleBands.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Services
{
    public class EventRepository : IRepository<ApplicationEvent>
    {
        ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Create(ApplicationEvent item)
        {
            _context.Events.Add(item);
        }

        public void Delete(string id)
        {
            ApplicationEvent ae = _context.Events.Find(id);
            if (ae != null) _context.Events.Remove(ae);
        }

        public ApplicationEvent Get(string id)
        {
            return _context.Events.Find(id);
        }

        public IEnumerable<ApplicationEvent> GetAll()
        {
            return _context.Events;
        }

        public IEnumerable<ApplicationEvent> GetAll(string id)
        {
            var result = new List<ApplicationEvent>();
            foreach (var events in this._context.Events)
            {
                if (events.E_UserId == id) result.Add(events);
            }
            return result;
        }

        public void Update(ApplicationEvent item)
        {
            var tmp = _context.Events.Find(item.EventId);
            tmp.EventDescription = item.EventDescription;
            tmp.EventName = item.EventName;
            tmp.EventPlace = item.EventPlace;
            tmp.EventTime = item.EventTime;
            tmp.E_UserId = item.E_UserId;
            
            _context.Entry(tmp).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
