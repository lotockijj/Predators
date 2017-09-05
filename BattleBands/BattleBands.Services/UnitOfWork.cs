﻿using BattleBands.Data;
using BattleBands.Models.ApplicationModels;
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
        private VideoRepository videoRepository;
        private PictureRepository photoRepository;
        private MusicRepository musicRepository;


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
        public MusicRepository Music
        {
            get
            {
                if (musicRepository == null)
                {
                    musicRepository = new MusicRepository(_context);
                }
                return musicRepository;
            }
        }
        public PictureRepository Picture
        {
            get
            {
                if (photoRepository == null) photoRepository = new PictureRepository(_context);
                return photoRepository;
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
        public VideoRepository Videos
        {
            get
            {
                if (videoRepository == null)
                {
                    videoRepository = new VideoRepository(_context);
                }
                return videoRepository;
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
