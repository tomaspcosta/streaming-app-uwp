using StreamingApp.Domain.Models;
using StreamingApp.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingApp.Domain.Repositories
{
    public interface ISeasonRepository : IRepository<Season>
    {
        Task<Season> FindByNumberAsync(int number);

        Task<List<Season>> FindAllWithDependenciesAsync();

        Task<int> CountNumberAsync(int seasonId);

        Task RemoveSeasonAsync(int number);

        Task<List<Season>> GetSeasonsForSeriesAsync(int seriesId);
    }
}
