using StreamingApp.Domain.Models;
using StreamingApp.Domain.SeedWork;
using System.Threading.Tasks;

namespace StreamingApp.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByUsernameAsync(string username, bool includePassword = false);
        Task<User> CreateNormalUserAsync(string username, string password, string email, string phoneNumber, bool isAdmin = false);
        Task RemoveUserAsync(string username);
        Task UpdateUserAsync(string username, string newUsername);
        Task<User> FindByUsernameAndPasswordAsync(string u, string p);
        Task<bool> IsNewUsernameUniqueAsync(string username);
        Task UpdateAsync(User user);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByPhoneNumberAsync(string phoneNumber);
    }
}