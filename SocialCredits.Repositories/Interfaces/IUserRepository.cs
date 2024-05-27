using SocialCredits.Domain.Models;

namespace SocialCredits.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> CreateUser(User user);
        public Task<User> GetUserByLogin(string login);
        public Task<List<User>> SearchUserByName(string name);
        public Task<List<User>> GetAllUsersList();
        public Task<long> GetUsersCount();
        public Task UpdateUserRole(string login, string role);
    }
}
