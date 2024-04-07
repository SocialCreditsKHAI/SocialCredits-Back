using MongoDB.Driver;
using SocialCredits.Domain;
using SocialCredits.Domain.Models;
using SocialCredits.Repositories.Interfaces;

namespace SocialCredits.Repositories.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(IMongoDbSettings settings) : base(settings, "User") { }

        public async Task<bool> CreateUser(User user)
        {
            try
            {
            await _collection.InsertOneAsync(user);
            return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User> GetUserByLogin(string login)
        {
            var user = await _collection.Find(Builders<User>.Filter.Eq("Login", login)).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<User>> SearchUserByName(string name)
        { 
            var filter = Builders<User>.Filter.Regex("Name", new MongoDB.Bson.BsonRegularExpression(name, "i"));
            return await _collection.Find<User>(filter).ToListAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
