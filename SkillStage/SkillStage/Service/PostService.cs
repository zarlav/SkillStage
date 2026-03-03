using MongoDB.Driver;
using SkillStage.Domain;
using SkillStage.DTO;
using SkillStage.Service.IService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillStage.Service
{
    public class PostService : IPostService
    {
        private readonly MongoDbService _mongoService;

        public PostService(MongoDbService mongoService)
        {
            _mongoService = mongoService;
        }

       
        public async Task CreatePostAsync(Post post)
        {
            await _mongoService.Posts.InsertOneAsync(post);
        }

        
        public async Task<IEnumerable<Post>> GetAllPostsAsync(PostType? type)
        {
            var filter = type.HasValue 
                ? Builders<Post>.Filter.Eq(p => p.Type, type.Value) 
                : Builders<Post>.Filter.Empty;

            return await _mongoService.Posts
                .Find(filter)
                .SortByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

       
        public async Task AddCommentAsync(CommentDTO dto, string userId)
        {
            var comment = new Comment
            {
                PostId = dto.PostId,
                UserId = userId,
                Content = dto.Content
            };

            await _mongoService.Comments.InsertOneAsync(comment);
        }

        public async Task AddRatingAsync(RatingDTO dto, string userId)
        {
            var rating = new Rating
            {
                PostId = dto.PostId,
                UserId = userId,
                Value = dto.Value
            };

            await _mongoService.Ratings.InsertOneAsync(rating);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(string postId)
        {
            return await _mongoService.Comments
                .Find(c => c.PostId == postId)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(string postId)
        {
            var ratings = await _mongoService.Ratings
                .Find(r => r.PostId == postId)
                .ToListAsync();

            if (ratings == null || !ratings.Any()) return 0;

            return ratings.Average(r => r.Value);
        }

        public async Task<Post?> GetPostByIdAsync(string id)
{
    return await _mongoService.Posts.Find(p => p.Id == id).FirstOrDefaultAsync();
}

public async Task UpdatePostAsync(string id, Post updatedPost)
{
    
    await _mongoService.Posts.ReplaceOneAsync(p => p.Id == id, updatedPost);
}

public async Task DeletePostAsync(string id)
{
    
    await _mongoService.Posts.DeleteOneAsync(p => p.Id == id);
}
    }
}