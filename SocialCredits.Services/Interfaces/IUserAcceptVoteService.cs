using SocialCredits.Domain.Enums;
using SocialCredits.Domain.Models;

namespace SocialCredits.Services.Interfaces
{
    public interface IUserAcceptVoteService
    {
        public Task<bool> CreateUserAcceptVote(UserAcceptVote userAcceptVote);
        public Task<VoteStatus> AddVoter(Voter userAcceptVote, string voteFor, int usersPercent);
        public Task<List<UserAcceptVote>> GetUsersOnVote();
        public Task<bool> DeleteUserAcceptVote(string userLogin);
    }
}
