using SocialCredits.Domain.Models;
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
    }
}
