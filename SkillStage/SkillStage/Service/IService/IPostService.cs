using SkillStage.Domain;
using SkillStage.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillStage.Service.IService
{
    public interface IPostService
    {
        Task CreatePostAsync(Post post);
        Task<IEnumerable<Post>> GetAllPostsAsync(PostType? type);
        
       
        Task<Post?> GetPostByIdAsync(string id); 
        Task UpdatePostAsync(string id, Post updatedPost);
        Task DeletePostAsync(string id);
        

        Task AddCommentAsync(CommentDTO dto, string userId);
        Task AddRatingAsync(RatingDTO dto, string userId);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(string postId);
        Task<double> GetAverageRatingAsync(string postId);
    }
}