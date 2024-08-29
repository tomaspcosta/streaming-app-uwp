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
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieListDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Movie>> FindAllByNameStartedWithAsync(string name)
        {
            return _dbContext.Movies
                .Where(x => x.Name.Contains(name) || x.Cast.Contains(name))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public Task<List<Movie>> FindAllByRatingAsync(int number)
        {
            return _dbContext.Movies
                .Where(x => x.Rating == number || x.Rating > number)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }


        public Task<List<Movie>> FindAllByCategory(int categoryId)
        {
            return _dbContext.Movies
                .Where(x => x.CategoryId == categoryId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }


        public Task<List<Movie>> FindAllByIsFavourite()
        {
            return _dbContext.Movies
                .Where(x => x.IsFavourite == true)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }



        public Task<List<Movie>> FindAllByCategoryAndTime(int categoryId, TimeSpan duration)
        {
            try
            {
                var movies = _dbContext.Movies
                    .Where(x => x.CategoryId == categoryId)
                    .AsEnumerable() // Switch to client-side operations
                    .Where(x => x.Duration <= duration)
                    .OrderBy(x => x.Name)
                    .ToList();

                return Task.FromResult(movies);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error in FindAllByCategoryAndTime: {ex.Message}", ex);
            }
        }



        public Task<Movie> FindByNameAsync(string name)
        {
            return _dbContext.Movies.SingleOrDefaultAsync(c => c.Name == name);
        }
        public override async Task<Movie> FindOrCreateAsync(Movie e)
        {

            var entity = await FindByNameAsync(e.Name);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }

        public async Task RemoveMovieAsync(string name)
        {
            var season = await _dbContext.Movies.SingleOrDefaultAsync(u => u.Name == name);
            if (season != null)
            {
                _dbContext.Movies.Remove(season);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
