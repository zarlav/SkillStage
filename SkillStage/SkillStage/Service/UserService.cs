using MongoDB.Driver;
using SkillStage.Domain;
using SkillStage.Service.IService;
using System;
using System.Threading.Tasks;

namespace SkillStage.Service
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(MongoDbService mongoDbService)
        {
           
            _users = mongoDbService.Database.GetCollection<User>("user");
        }

      
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _users.Find(x => x.UserName == username).FirstOrDefaultAsync();
        }

     
        public async Task<User?> GetByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

       
        public async Task CreateAsync(User user)
        {
           
            var existingUser = await _users.Find(x => x.UserName == user.UserName || x.Email == user.Email).FirstOrDefaultAsync();
            
            if (existingUser != null)
            {
                
                throw new Exception("Korisnik sa tim korisničkim imenom ili emailom već postoji.");
            }

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