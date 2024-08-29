using StreamingApp.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace StreamingApp.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISeasonRepository SeasonRepository { get; }
        IEpisodesRepository EpisodesRepository { get; }
        ISeriesRepository SeriesRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
        Task UpdateUserAsync(string username, string newUsername);
    }
}
