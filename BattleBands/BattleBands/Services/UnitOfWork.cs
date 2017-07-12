using BattleBands.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Services
{
    public class UnitOfWork : IDisposable
    {
        ApplicationDbContext _context;
        private PerformerRepository performerRepository;
        private EventRepository eventRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public PerformerRepository Performers
        {
            get
            {
                if (performerRepository == null)
                { 
                    performerRepository = new PerformerRepository(_context);
                }
                return performerRepository;
            }
        }

        public EventRepository Events
        {
            get
            {
                if (eventRepository == null)
                {
                    eventRepository = new EventRepository(_context);
                }
                return eventRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
