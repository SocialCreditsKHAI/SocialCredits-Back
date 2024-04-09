using SocialCredits.Domain.Models;

namespace SocialCredits.Services.Interfaces
{
    public interface IUserAcceptVoteService
    {
        public Task<bool> CreateUserAcceptVote(UserAcceptVote userAcceptVote);
        public Task<bool> AddVoter(UserAcceptVote userAcceptVote);
        public Task<List<UserAcceptVote>> GetUsersOnVote();
        public Task<bool> DeleteUserAcceptVote(string userLogin);
    }
}
