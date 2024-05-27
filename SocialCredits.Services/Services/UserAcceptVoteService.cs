using MongoDB.Driver.Core.Authentication;
using SocialCredits.Domain.Models;
using SocialCredits.Repositories.Interfaces;
using SocialCredits.Services.Interfaces;
using SocialCredits.Domain.Enums;


namespace SocialCredits.Services.Services
{
    public class UserAcceptVoteService : IUserAcceptVoteService
    {
        private readonly IUserAcceptVoteRepository _repository;

        public UserAcceptVoteService(IUserAcceptVoteRepository repository)
        {
            _repository = repository;
        }


        public async Task<VoteStatus> AddVoter(Voter userAcceptVote, string voteFor, int usersPercent)
        {
            var vote = await _repository.GetOneFromLogin(voteFor);
            if (vote == null)
            {
                return VoteStatus.NotFound;
            }
            if (vote.Voters.Find(v => v.VoterLogin == userAcceptVote.VoterLogin) != null)
            {
                return VoteStatus.AlreadyVoted;
            }
            vote.Voters.Add(userAcceptVote);
            if (vote.GetVotersStatistic().Accept >= usersPercent)
            {
                await _repository.Delete(voteFor);
                return VoteStatus.Approved;
            }
            if (vote.GetVotersStatistic().Unaccept >= usersPercent)
            {
                await _repository.Delete(voteFor);
                return VoteStatus.Unapproved;
            }
            await _repository.UpdateVoters(voteFor, userAcceptVote);
            return VoteStatus.Continue;
        }

        public async Task<bool> CreateUserAcceptVote(UserAcceptVote userAcceptVote)
        {
            try
            {
                await _repository.Create(userAcceptVote);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> DeleteUserAcceptVote(string userLogin)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserAcceptVote>> GetUsersOnVote()
        {
            return await _repository.GetUsersOnVote();

        }
    }
}

