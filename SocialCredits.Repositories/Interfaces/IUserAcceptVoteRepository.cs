using SocialCredits.Domain.Models;
using SocialCredits.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Repositories.Interfaces
{
    public interface IUserAcceptVoteRepository
    {
        public Task Create(UserAcceptVote userAcceptVote);
        public Task<List<UserAcceptVote>> GetUsersOnVote();
        public Task<UserAcceptVote> GetOneFromLogin(string login);
        public Task Delete(string login);
        Task UpdateVoters(string VoteForLogin, Voter voter);
    }
}
