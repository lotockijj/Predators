using BattleBands.Data;
using BattleBands.Models.ApplicationModels;
using BattleBands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Services
{
    public class PictureRepository : IRepository<ApplicationPhoto>
    {
        ApplicationDbContext context;
        public PictureRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void Create(ApplicationPhoto item)
        {
            context.Photo.Add(item);
        }

        public void Delete(string id)
        {
            var dlt = Get(id);
            context.Photo.Remove(dlt);
        }

        public ApplicationPhoto GetLastByOwner(string id)
        {
            var result = new ApplicationPhoto();
            var list = context.Photo.OrderBy(x => x.UploadTime).AsEnumerable();
            foreach (var item in list)
            {
                if (item.IdOwner == id) result = item;
            }
            return result;
        }

        public ApplicationPhoto Get(string id)
        {
            return context.Photo.Find(id);
        }

        public IEnumerable<ApplicationPhoto> GetAll()
        {
            var lst = new List<ApplicationPhoto>();
            foreach (var item in context.Photo)
            {
                lst.Add(item);
            }
            return lst;
        }

        public void Update(ApplicationPhoto item)
        {
            var tmp = context.Photo.Find(item.Id);
            tmp.IdOwner = item.IdOwner;
            tmp.Name = item.Name;
            tmp.Path = item.Path;
            tmp.UploadTime = DateTimeOffset.Now;

            context.Entry(tmp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
    }
}
