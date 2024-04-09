using Microsoft.VisualBasic;
using MongoDB.Driver;
using SocialCredits.Domain;
using SocialCredits.Domain.Models;
using SocialCredits.Repositories.Interfaces;

namespace SocialCredits.Repositories.Repository
{
    public class UserAcceptVoteRepository : BaseRepository<UserAcceptVote>, IUserAcceptVoteRepository
    {
        public UserAcceptVoteRepository(IMongoDbSettings settings) : base(settings, "UserAcceptVote") { }

        public async Task Create(UserAcceptVote userAcceptVote)
        {
            await _collection.InsertOneAsync(userAcceptVote);
        }

        public async Task<List<UserAcceptVote>> GetUsersOnVote()
        {
            var response = await _collection.Find(Builders<UserAcceptVote>.Filter.Empty).ToListAsync();
            return response;
        }
    }
}
