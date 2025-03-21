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
    public class SeriesRepository : Repository<SeriesClass>, ISeriesRepository
    {
        public SeriesRepository(MovieListDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<SeriesClass>> FindAllByNameStartedWithAsync(string name)
        {
            return _dbContext.Series.Include(x => x.Seasons)
                .Where(x => x.Name.Contains(name) || x.Cast.Contains(name))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public Task<List<SeriesClass>> FindAllByRatingAsync(int number)
        {
            return _dbContext.Series
                .Where(x => x.Rating == number || x.Rating > number)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public Task<List<SeriesClass>> FindAllByCategory(int categoryId)
        {
            return _dbContext.Series
                .Where(x => x.CategoryId == categoryId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public Task<List<SeriesClass>> FindAllByIsFavourite()
        {
            return _dbContext.Series
                .Where(x => x.IsFavourite == true)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }


        public Task<List<SeriesClass>> FindAllByCategoryAndTime(int categoryId, TimeSpan duration)
        {
            try
            {
                var movies = _dbContext.Series
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

        public Task<List<SeriesClass>> FindAllWithDependenciesAsync()
        {
            return _dbContext.Series.Include(x => x.Seasons).ToListAsync();
        }

        public Task<SeriesClass> FindByNameAsync(string name)
        {
            return _dbContext.Series.SingleOrDefaultAsync(c => c.Name == name);
        }

        public Task<SeriesClass> FindById(int id)
        {
            return _dbContext.Series.SingleOrDefaultAsync(c => c.Id == id);
        }


        public override async Task<SeriesClass> FindOrCreateAsync(SeriesClass e)
        {
            var entity = await FindByNameAsync(e.Name);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }

        public async Task RemoveSeriesAsync(string name)
        {
            var serie = await _dbContext.Series.SingleOrDefaultAsync(u => u.Name == name);
            if (serie != null)
            {
                _dbContext.Series.Remove(serie);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
