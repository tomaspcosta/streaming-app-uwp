using StreamingApp.Domain.Models;
using StreamingApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingApp.Domain.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> FindByNameAsync(string name);

        Task<List<Movie>> FindAllByNameStartedWithAsync(string name);
        Task<List<Movie>> FindAllByRatingAsync(int number);

        Task<List<Movie>> FindAllByCategory(int categoryId);

        Task<List<Movie>> FindAllByCategoryAndTime(int categoryId, TimeSpan duration);

        Task<List<Movie>> FindAllByIsFavourite();

        Task RemoveMovieAsync(string name);
    }
}
