using MongoDB.Driver;
using SkillStage.Domain;

namespace SkillStage.Service
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;

        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("DbConnection");
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database => _database;

        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("comments");
        public IMongoCollection<Rating> Ratings => _database.GetCollection<Rating>("ratings");
    }
}