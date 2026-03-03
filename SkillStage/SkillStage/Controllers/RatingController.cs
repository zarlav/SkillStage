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
    public class RatingController : ControllerBase
    {
        private readonly IPostService _postService;

        public RatingController(IPostService postService)
        {
            _postService = postService;
        }

       
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RatePost([FromBody] RatingDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Niste ulogovani.");

            try
            {
                await _postService.AddRatingAsync(dto, userId);
                return Ok(new { message = "Uspešno ste ocenili post!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
        [HttpGet("Average/{postId}")]
        public async Task<IActionResult> GetAverage(string postId)
        {
            var average = await _postService.GetAverageRatingAsync(postId);
            
            
            return Ok(new { 
                postId = postId, 
                averageRating = average 
            });
        }
    }
}