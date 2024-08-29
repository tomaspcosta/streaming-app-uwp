using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain;
using StreamingApp.Domain.Repositories;
using StreamingApp.Infrastructure;
using StreamingApp.InfraStructure.Repositories;
using System.Threading.Tasks;

namespace StreamingApp.InfraStructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public MovieListDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new MovieListDbContext();

            // Create database if not exists yet
            _dbContext.Database.EnsureCreated();

            // Apply a migration
            _dbContext.Database.Migrate();
        }

        public IMovieRepository MovieRepository => new MovieRepository(_dbContext);
        public ICategoryRepository CategoryRepository => new CategoryRepository(_dbContext);
        public ISeriesRepository SeriesRepository => new SeriesRepository(_dbContext);
        public IEpisodesRepository EpisodesRepository => new EpisodesRepository(_dbContext);
        public ISeasonRepository SeasonRepository => new SeasonsRepository(_dbContext);
        public IUserRepository UserRepository => new UserRepository(_dbContext);
        public IFavouritesRepository FavouritesRepository => new FavouritesRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(string username, string newUsername)
        {
            await UserRepository.UpdateUserAsync(username, newUsername);
            await SaveAsync();
        }
    }
}
