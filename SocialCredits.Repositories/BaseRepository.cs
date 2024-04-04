using MongoDB.Driver;
using SocialCredits.Domain;

namespace SocialCredits.Repositories
{
    public class BaseRepository<T>
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoDbSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(collectionName);
        }
    }
}
