using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Models;
using StreamingApp.Domain.Repositories;
using StreamingApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingApp.InfraStructure.Repositories
{
    public class SeasonsRepository : Repository<Season>, ISeasonRepository
    {
        public SeasonsRepository(MovieListDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Season>> FindAllWithDependenciesAsync()
        {
            return _dbContext.Seasons.Include(x => x.Episodes).ToListAsync();
        }

        public Task<Season> FindByNumberAsync(int number)
        {
            return _dbContext.Seasons.SingleOrDefaultAsync(c => c.Number == number);
        }

        public override async Task<Season> FindOrCreateAsync(Season e)
        {
            var entity = await FindByNumberAsync(e.Number);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }

        public async Task RemoveSeasonAsync(int number)
        {
            var season = await _dbContext.Seasons.SingleOrDefaultAsync(u => u.Number == number);
            if (season != null)
            {
                _dbContext.Seasons.Remove(season);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> CountNumberAsync(int seasonId)
        {
            return await _dbContext.Episodes
                .Where(e => e.SeasonId == seasonId)
                .CountAsync();
        }

        public Task<List<Season>> GetSeasonsForSeriesAsync(int seriesId)
        {
            return _dbContext.Seasons
                .Where(s => s.SeriesId == seriesId)
                .ToListAsync();
        }


    }
}
