using SocialCredits.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> CreateUser(User user);
        public Task<bool> UpdateUser(User user);
        public Task<User> GetUserByLogin(string login);
        public Task<List<User>> SearchUserByName(string name);
    }
}
