using StreamingApp.Domain.Models;
using StreamingApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingApp.Domain.Repositories
{
    public interface ISeriesRepository : IRepository<SeriesClass>
    {
        Task<SeriesClass> FindByNameAsync(string name);

        Task<List<SeriesClass>> FindAllByNameStartedWithAsync(string name);

        Task<List<SeriesClass>> FindAllWithDependenciesAsync();
        Task<List<SeriesClass>> FindAllByCategory(int categoryId);

        Task<List<SeriesClass>> FindAllByRatingAsync(int number);
        Task<List<SeriesClass>> FindAllByIsFavourite();
        Task<List<SeriesClass>> FindAllByCategoryAndTime(int categoryId, TimeSpan duration);
        Task RemoveSeriesAsync(string name);
        Task<SeriesClass> FindById(int id);
    }
}
