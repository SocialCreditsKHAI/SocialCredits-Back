using SocialCredits.Domain.Models;
using SocialCredits.Repositories.Interfaces;
using SocialCredits.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Services.Services
{
    public class UserAcceptVoteService : IUserAcceptVoteService
    {
        private readonly IUserAcceptVoteRepository _repository;

        public UserAcceptVoteService(IUserAcceptVoteRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> AddVoter(UserAcceptVote userAcceptVote)
        {
            throw new NotImplementedException();
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

