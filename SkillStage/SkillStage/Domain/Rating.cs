using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkillStage.Domain
{
    public class Rating
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("postId"), BsonRepresentation(BsonType.ObjectId)]
        public string? PostId { get; set; }

        [BsonElement("userId"), BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [BsonElement("value"), BsonRepresentation(BsonType.Int32)]
        public int Value { get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}