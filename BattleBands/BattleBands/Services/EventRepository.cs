﻿using BattleBands.Data;
using BattleBands.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Services
{
    //public class EventRepository : IRepository<ApplicationEvent>
    //{
    //    ApplicationDbContext _context;

    //    public EventRepository(ApplicationDbContext context)
    //    {
    //        this._context = context;
    //    }

    //    public void Create(ApplicationEvent item)
    //    {
    //        _context.Events.Add(item);
    //    }

    //    public void Delete(string id)
    //    {
    //        ApplicationEvent ap = _context.Events.Find(id);
    //        if (ap != null) _context.Events.Remove(ap);
    //    }

    //    public ApplicationEvent Get(string id)
    //    {
    //        return _context.Events.Find(id);
    //    }

    //    public IEnumerable<ApplicationEvent> GetAll()
    //    {
    //        return _context.Events;
    //    }

    //    //public IEnumerable<ApplicationEvent> GetAll(string id)
    //    //{
    //    //    var result = new List<ApplicationEvent>();
    //    //    foreach (var perf in this._context.Events)
    //    //    {
    //    //        if (perf.UserId == id) result.Add(perf);
    //    //    }
    //    //    return result;
    //    //}

    //    public void Update(ApplicationEvent item)
    //    {
    //        _context.Entry(item).State = EntityState.Modified;
    //    }
    //}
}
