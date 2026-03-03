using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SkillStage.DTO;
using System.Security.Claims;
using System.Threading.Tasks;
using SkillStage.Service.IService;
using SkillStage.DTOs;      
using SkillStage.Domain; 


    namespace SkillStage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IPostService _postService;

        public CommentController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Niste ulogovani.");

            try 
            {
                await _postService.AddCommentAsync(dto, userId);
                return Ok(new { message = "Komentar je dodat!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Post/{postId}")]
        public async Task<IActionResult> GetCommentsByPost(string postId)
        {
            var comments = await _postService.GetCommentsByPostIdAsync(postId);
            
            if (comments == null)
                return Ok(new List<Comment>());

            return Ok(comments);
        }
    }
}