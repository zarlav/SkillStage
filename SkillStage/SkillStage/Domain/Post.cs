using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkillStage.Domain
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!;

        [BsonElement("type")]
        public PostType Type { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; } // opciono,za linkove ka slikama/muzici

        [BsonElement("content")]
        public string Content { get; set; } = null!;
        // crtez -> URL slike
        // citat -> tekst
        // muzika -> link

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
