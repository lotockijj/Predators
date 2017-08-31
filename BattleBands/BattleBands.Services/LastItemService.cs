using System;
using System.Collections.Generic;
using System.Text;
using BattleBands.Data;
using BattleBands.Interfaces;
using BattleBands.Models.ApplicationModels;
using BattleBands.Models;

namespace BattleBands.Services
{
    public class LastItemService : ILastItem<RecentItems>
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ApplicationDbContext context;
        public LastItemService(ApplicationDbContext _context)
        {
            unitOfWork = new UnitOfWork(_context);
            context = _context;
        }

        public RecentItems GetLast()
        {
            var result = new RecentItems
            {
                Performer = unitOfWork.Performers.GetLast(),
                Event = unitOfWork.Events.GetLast(),
                Music = unitOfWork.Music.GetLast(),
                Video = unitOfWork.Videos.GetLast()
            };
            result.PerformerPath = unitOfWork.Picture.GetLastByOwner(result.Performer.PerformerId).Path;
            result.EventPath = unitOfWork.Picture.GetLastByOwner(result.Event.EventId).Path;

            return result;
        }
    }
}
