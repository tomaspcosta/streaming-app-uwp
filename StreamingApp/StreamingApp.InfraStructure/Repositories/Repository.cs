using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.SeedWork;
using StreamingApp.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingApp.InfraStructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {


        protected readonly MovieListDbContext _dbContext;

        protected Repository(MovieListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T e)
        {
            _dbContext.Add(e);
        }

        public void Delete(T e)
        {
            _dbContext.Remove(e);
        }
        public Task<List<T>> FindAllAsync()
        {
            return _dbContext.Set<T>().ToListAsync();

        }

        public Task<T> FindByIdAsync(int id)
        {
            return _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public abstract Task<T> FindOrCreateAsync(T e);

        public void Update(T e)
        {
            _dbContext.Update(e);
        }
    }
}
