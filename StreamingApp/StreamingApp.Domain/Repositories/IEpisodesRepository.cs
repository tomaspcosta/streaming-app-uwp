using StreamingApp.Domain.Models;
using StreamingApp.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingApp.Domain.Repositories
{
    public interface IEpisodesRepository : IRepository<Episodes>
    {
        Task<Episodes> FindByNumberAsync(int number);

        Task<List<Episodes>> FindAllWithDependenciesAsync();

        Task<int> CountNumberAsync();

        Task RemoveEpisodeAsync(int number);
    }
}
