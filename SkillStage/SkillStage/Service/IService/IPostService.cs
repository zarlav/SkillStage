using SkillStage.Domain;
using SkillStage.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillStage.Service.IService
{
    public interface IPostService
    {
        Task AddCommentAsync(CommentDTO dto, string userId);
        Task AddRatingAsync(RatingDTO dto, string userId);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(string postId);
        Task<double> GetAverageRatingAsync(string postId);
    }
}