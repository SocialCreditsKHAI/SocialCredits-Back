using SocialCredits.Domain.DTO;
using SocialCredits.Domain.Models;
using SocialCredits.Domain.ViewModels;
using System.Net;

namespace SocialCredits.Services.Interfaces
{
    public interface IUserServices
    {
        public User GetUserByLogin(int id);
        public User GetUserByName(string name);
        public Task<(HttpStatusCode, string)> Login(UserLoginDTO user);
        public Task<bool> Registration(UserRegistrationWithImageNameViewModel model);
        public Task<bool> Registration(UserRegistrationWithImageViewModel model);


    }
}
