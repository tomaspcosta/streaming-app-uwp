using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Models;
using StreamingApp.Domain.Repositories;
using StreamingApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingApp.InfraStructure.Repositories
{
    public class FavouritesRepository : Repository<Favourites>, IFavouritesRepository
    {
        public void Add(Favourites favourites)
        {
            _dbContext.Set<Favourites>().Add(favourites);
        }
        public FavouritesRepository(MovieListDbContext dbContext) : base(dbContext)
        {
        }
        public Task<Favourites> GetByIdAsync(int favouritesId)
        {
            return _dbContext.Favourites.FirstOrDefaultAsync(c => c.Id == favouritesId);
        }

        public Task<List<Favourites>> FindAllFavouritesAsync()
        {
            return _dbContext.Favourites
                .Include(favorite => favorite.FavoriteMovies)
                    .Include(favorite => favorite.FavoriteSeries)
                .ToListAsync();
        }

        public override async Task<Favourites> FindOrCreateAsync(Favourites e)
        {
            var entity = await FindByIdAsync(e.UserId);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }

        public Task<Favourites> FindByUserIdAsync(int UserId)
        {
            return _dbContext.Favourites
        .Include(f => f.FavoriteMovies)
        .Include(f => f.FavoriteSeries)
        .SingleOrDefaultAsync(c => c.UserId == UserId);
        }

        public async Task AddMovieToFavoritesAsync(int userId, string movieName)
        {
            var user = await _dbContext.Favourites
                .Include(u => u.FavoriteMovies)
                .SingleOrDefaultAsync(u => u.UserId == userId);

        }



        public async Task AddSeriesToFavoritesAsync(int userid, SeriesClass series)
        {
            var user = await _dbContext.Favourites
                .Include(u => u.FavoriteMovies)
                .Include(u => u.FavoriteSeries)
                .SingleOrDefaultAsync(u => u.UserId == userid);

            if (user != null)
            {
                Console.WriteLine($"User found: {user.UserId}");

                if (user.FavoriteMovies == null)
                {
                    user.FavoriteMovies = new List<Movie>();
                }

                if (user.FavoriteSeries == null)
                {
                    user.FavoriteSeries = new List<SeriesClass>();
                }

                Console.WriteLine($"FavoriteMovies: {user.FavoriteMovies?.Count}");
                Console.WriteLine($"FavoriteSeries: {user.FavoriteSeries?.Count}");

                // Check if the series is already in the user's favorites to avoid duplicates
                if (!user.FavoriteSeries.Select(s => s.Id).Contains(series.Id))
                {
                    user.FavoriteSeries.Add(series);
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("Changes saved to the database.");
                }
            }
        }
    }
}
