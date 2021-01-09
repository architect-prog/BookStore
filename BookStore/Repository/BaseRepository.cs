using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    interface BaseRepository<T>
    {
        Task<int> Add(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);          
    }
}
