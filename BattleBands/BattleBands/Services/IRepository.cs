using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Services
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetAll(string id);
        T Get(string id);
        void Create(T item);
        void Update(T item);
        void Delete(string id);
    }
}
