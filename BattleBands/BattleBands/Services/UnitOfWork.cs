using BattleBands.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Services
{
    public class UnitOfWork : IDisposable
    {
        private PerformerContext db = new PerformerContext();
        private PerformerRepository performerRepository;

        public PerformerRepository Performers
        {
            get
            {
                if (performerRepository == null)
                { 
                    performerRepository = new PerformerRepository(db);
                }
                return performerRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
