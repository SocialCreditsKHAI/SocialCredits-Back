using SocialCredits.Domain.DTO;
using SocialCredits.Domain.Models;
using SocialCredits.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Services
{
    public class UserService : IUserServices
    {
        public User GetUserByLogin(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public string Login(UserLoginDTO user)
        {
            throw new NotImplementedException();
        }

        public string Registrationg(User user)
        {
            throw new NotImplementedException();
        }
    }
}
