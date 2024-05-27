using SocialCredits.Domain.DTO;
using SocialCredits.Domain.Models;
using SocialCredits.Domain.ViewModels;
using System.Net;

namespace SocialCredits.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<User> GetUserByLogin(string login);
        public User GetUserByName(string name);
        public Task<(HttpStatusCode StatusCode, string Message)> Login(UserLoginDTO user);
        public Task<bool> Registration(UserRegistrationWithImageNameViewModel model);
        public Task<bool> Registration(UserRegistrationWithImageViewModel model);
        public Task<List<UserToShowViewModel>> GetAllUsersList();
        public Task<long> GetUsersCount();
        public Task ChangeRoleToUser(string login);
        Task<UserToShowViewModel> GetUserToShow(string login);
    }
}
