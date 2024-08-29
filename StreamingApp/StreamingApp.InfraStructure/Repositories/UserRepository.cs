using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Models;
using StreamingApp.Domain.Repositories;
using StreamingApp.Infrastructure;
using System;
using System.Threading.Tasks;

namespace StreamingApp.InfraStructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private int lastUserId;

        public UserRepository(MovieListDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindByUsernameAsync(string username, bool includePassword = false)
        {
            var query = _dbContext.Users.AsQueryable();

            if (!includePassword)
            {
                query = query.AsNoTracking();
            }

            return await query.SingleOrDefaultAsync(u => u.Username == username);
        }

        public override Task<User> FindOrCreateAsync(User e)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> CreateNormalUserAsync(string username, string password, string email, string phoneNumber, bool isAdmin = false)
        {
            var existingUser = await FindByUsernameAsync(username);
            if (existingUser != null)
            {
                return null;
            }

            var user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                PhoneNumber = phoneNumber,
                IsAdmin = isAdmin,
            };

            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }


        public async Task RemoveUserAsync(string username)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(string username, string newUsername)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                user.Username = newUsername;
            }
        }

        public Task<User> FindByUsernameAndPasswordAsync(string u, string p)
        {
            return _dbContext.Users.SingleOrDefaultAsync(c => c.Username == u && c.Password == p);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _dbContext.Set<User>().SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> FindByPhoneNumberAsync(string phoneNumber)
        {
            return await _dbContext.Set<User>().SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<bool> IsNewUsernameUniqueAsync(string username)
        {
            return await _dbContext.Set<User>().AllAsync(u => u.Username != username);
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                var existingUser = await _dbContext.Users.FindAsync(user.Id);

                if (existingUser != null)
                {
                    existingUser.Email = user.Email;
                    existingUser.Username = user.Username;
                    existingUser.PhoneNumber = user.PhoneNumber;

                    if (user.Password != null)
                    {
                        existingUser.Password = user.Password;
                    }

                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("User not found", nameof(user));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating user information: {ex.Message}");
                throw;
            }
        }
    }
}