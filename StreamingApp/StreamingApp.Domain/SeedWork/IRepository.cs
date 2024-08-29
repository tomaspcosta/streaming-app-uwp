using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingApp.Domain.SeedWork
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T e);
        void Update(T e);
        void Delete(T e);
        //void Find(T e);
        Task<T> FindOrCreateAsync(T e);
        Task<List<T>> FindAllAsync();
        Task<T> FindByIdAsync(int id);


    }
}
