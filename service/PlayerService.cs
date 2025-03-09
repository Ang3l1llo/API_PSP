using TodoApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace TodoApi.Services
{
    public class PlayerService
    {
        private readonly IMongoCollection<Player> _playersCollection;

        public PlayerService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playersCollection = mongoDatabase.GetCollection<Player>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Player>> GetTop5PlayersAsync() =>
            await _playersCollection.Find(_ => true)
                                      .SortByDescending(j => j.Puntuacion)
                                      .Limit(5)
                                      .ToListAsync();

        public async Task<List<Player>> GetAllAsync() =>
            await _playersCollection.Find(_ => true).ToListAsync();

        public async Task<Player> GetByIdAsync(string id) =>
            await _playersCollection.Find(j => j.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(Player player) =>
            await _playersCollection.InsertOneAsync(player);

        public async Task UpdateAsync(string id, Player player) =>
            await _playersCollection.ReplaceOneAsync(j => j.Id.Equals(id), player);

        public async Task DeleteAsync(string id) =>
            await _playersCollection.DeleteOneAsync(j => j.Id.Equals(id));
    }
}