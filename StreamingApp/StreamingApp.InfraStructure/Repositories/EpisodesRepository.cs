using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Models;
using StreamingApp.Domain.Repositories;
using StreamingApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingApp.InfraStructure.Repositories
{
    public class EpisodesRepository : Repository<Episodes>, IEpisodesRepository
    {
        public EpisodesRepository(MovieListDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Episodes>> FindAllWithDependenciesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Episodes> FindByNumberAsync(int number)
        {
            return _dbContext.Episodes.SingleOrDefaultAsync(c => c.Number == number);
        }

        public async Task<int> CountNumberAsync()
        {
            return await _dbContext.Episodes.CountAsync();

        }


        public override async Task<Episodes> FindOrCreateAsync(Episodes e)
        {
            // Attempt to find the episode by its number
            var entity = await FindByNumberAsync(e.Number);

            if (entity == null)
            {
                // If not found, create the episode with the incremented number
                Create(new Episodes
                {
                    Name = e.Name,
                    Rating = e.Rating,
                    Description = e.Description,
                    SeasonId = e.SeasonId,
                    Season = e.Season
                });

                entity = e;
            }

            return entity;
        }



        public async Task RemoveEpisodeAsync(int number)
        {
            var episode = await _dbContext.Episodes.SingleOrDefaultAsync(u => u.Number == number);
            if (episode != null)
            {
                _dbContext.Episodes.Remove(episode);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
