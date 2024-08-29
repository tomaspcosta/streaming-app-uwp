using StreamingApp.Domain.Models;
using StreamingApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.Domain.Repositories
{
    public interface IFavouritesRepository : IRepository<Favourites>
    {
        Task AddMovieToFavoritesAsync(int userId, string movieName);

        Task AddSeriesToFavoritesAsync(int userid, SeriesClass series);

        Task<List<Favourites>> FindAllFavouritesAsync();

        Task<Favourites> GetByIdAsync(int favouritesId);

        Task<Favourites> FindByUserIdAsync(int UserId);

        void Add(Favourites favourites);

    }
}
