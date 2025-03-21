using StreamingApp.Domain.Models;
using StreamingApp.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingApp.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> FindByNameAsync(string name);

        Task<List<Category>> FindAllByNameStartedWithAsync(string name);

        Task<List<Category>> FindAllByNameStartedWithAsyncMovieSeries(string name);

        Task<Category> GetByIdAsync(int categoryId);

        Task<List<Category>> FindAllWithDependenciesAsync();

    }

}
