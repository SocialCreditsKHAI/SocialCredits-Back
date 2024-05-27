using DnsClient;
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

        public async Task Delete(string login)
        {
            await _collection.DeleteOneAsync(Builders<UserAcceptVote>.Filter.Eq("UserLogin", login));
        }

        public async Task<UserAcceptVote> GetOneFromLogin(string login)
        {
            return await _collection.Find(Builders<UserAcceptVote>.Filter.Eq("UserLogin", login)).FirstOrDefaultAsync();
        }

        public async Task UpdateVoters(string VoteForLogin, Voter voter)
        {
            var filter = Builders<UserAcceptVote>.Filter.Eq("UserLogin", VoteForLogin);
            var update = Builders<UserAcceptVote>.Update.Push("Voters", voter);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<List<UserAcceptVote>> GetUsersOnVote()
        {
            var response = await _collection.Find(Builders<UserAcceptVote>.Filter.Empty).ToListAsync();
            return response;
        }
    }
}
