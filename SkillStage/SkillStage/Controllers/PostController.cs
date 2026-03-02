using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SkillStage.DTO;
using System.Security.Claims;
using System.Threading.Tasks;
using SkillStage.Service.IService;

namespace SkillStage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostController 
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostController(IPostService postService, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO dto)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return new UnauthorizedObjectResult("Korisnik nije autentifikovan");

            await _postService.AddCommentAsync(dto, userId);
            return new OkObjectResult(new { message = "Komentar je uspesno dodat" });
        }

        [HttpPost("rate")]
        public async Task<IActionResult> AddRating([FromBody] RatingDTO dto)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return new UnauthorizedObjectResult("Korisnik nije autentifikovan");

            if (dto.Value < 1 || dto.Value > 5)
                return new BadRequestObjectResult("Ocena mora biti izmedju 1 i 5");

            await _postService.AddRatingAsync(dto, userId);
            return new OkObjectResult(new { message = "Ocena je uspesno dodata" });
        }

        [HttpGet("{postId}/comments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComments(string postId)
        {
            var comments = await _postService.GetCommentsByPostIdAsync(postId);
            return new OkObjectResult(comments);
        }

        [HttpGet("{postId}/average-rating")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAverageRating(string postId)
        {
            var average = await _postService.GetAverageRatingAsync(postId);
            return new OkObjectResult(new { averageRating = average });
        }
    }
}