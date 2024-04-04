using SocialCredits.Domain.DTO;
using SocialCredits.Domain.Models;

namespace SocialCredits.Services.Interfaces
{
    public interface IUserServices
    {
        public User GetUserByLogin(int id);
        public User GetUserByName(string name);
        public string Login(UserLoginDTO user);
        public string Registrationg(User user);


    }
}
