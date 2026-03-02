using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkillStage.Domain
{
    public class User
    {
        [BsonId]
        [BsonElement("id"),BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string? Name { get; set; }
        [BsonElement("last_name"), BsonRepresentation(BsonType.String)]
        public string? LastName { get; set; }
        [BsonElement("username"), BsonRepresentation(BsonType.String)]
        public string? UserName { get; set; }
        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }
        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string? Password { get; set; }


    }
}
