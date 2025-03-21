using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Models;
using StreamingApp.Domain.Repositories;
using StreamingApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingApp.InfraStructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MovieListDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Category>> FindAllByNameStartedWithAsync(string name)
        {
            return _dbContext.Categories
                .Where(x => x.Name.StartsWith(name))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public Task<List<Category>> FindAllByNameStartedWithAsyncMovieSeries(string name)
        {
            return _dbContext.Categories
                .Where(x => x.Name.StartsWith(name))
                .Include(x => x.Movies)
                .Include(x => x.Series)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public Task<Category> FindByNameAsync(string name)
        {
            return _dbContext.Categories.SingleOrDefaultAsync(c => c.Name == name);
        }
        public Task<Category> GetByIdAsync(int categoryId)
        {
            return _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public override async Task<Category> FindOrCreateAsync(Category e)
        {
            var entity = await FindByNameAsync(e.Name);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }

        public Task<List<Category>> FindAllWithDependenciesAsync()
        {
            return _dbContext.Categories.Include(x => x.Movies).Include(x => x.Series).ToListAsync();

        }
    }
}
