using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    interface IRepository<T> //: IDisposable
        where T : class
    {
        IEnumerable<T> GetList(); 

        T GetItem(Guid id);

        //void Create(T item);

        void Update(T item);

        void Add(T item);

        void Delete(T item); 
    }
}
