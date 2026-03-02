using MongoDB.Driver;
using SkillStage.Domain;
using SkillStage.Service.IService;

namespace SkillStage.Service
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(MongoDbService mongoDbService)
        {
            _users = mongoDbService.Database.GetCollection<User>("user");
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<bool> UpdateAsync(string id, User user)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            var result = await _users.ReplaceOneAsync(filter, user);
            return result.MatchedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            var result = await _users.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
