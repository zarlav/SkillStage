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
    [Authorize]
    public class PostController : ControllerBase
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
        // Kreiranje posta
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDTO dto)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return new UnauthorizedObjectResult("Niste ulogovani");

            var post = new Post
            {
                UserId = userId,
                Title = dto.Title,
                Content = dto.Content,
                Type = (PostType)dto.Type,
                ImageUrl = dto.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            await _postService.CreatePostAsync(post); 
            return new OkObjectResult(new { message = "Objava je uspešno kreirana", postId = post.Id });
        }

        //Dobavljanje svih postova sa opcionim filtriranjem po tipu
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPosts([FromQuery] PostType? type)
        {
            
            var posts = await _postService.GetAllPostsAsync(type);
            return new OkObjectResult(posts);
        }

       [HttpPut("{id}")]
public async Task<IActionResult> UpdatePost(string id, PostCreateDTO dto)
{
    // Koristimo ClaimsPrincipal za izvlačenje ID-a
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var post = await _postService.GetPostByIdAsync(id);

    if (post == null) return NotFound("Post nije pronađen.");
    
    if (post.UserId != userId) return Forbid();

    post.Title = dto.Title;
    post.Content = dto.Content;
    
    
    post.Type = (PostType)dto.Type; 
    
    post.ImageUrl = dto.ImageUrl;

    await _postService.UpdatePostAsync(id, post);
    return Ok(post);
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeletePost(string id)
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var post = await _postService.GetPostByIdAsync(id);

    if (post == null) return NotFound();
    if (post.UserId != userId) return Forbid();

    await _postService.DeletePostAsync(id);
    return Ok(new { message = "Post uspešno obrisan" });
}
 
   [HttpGet("User/{userId}")]
public async Task<IActionResult> GetPostsByUser(string userId)
{
    var allPosts = await _postService.GetAllPostsAsync(null);
    var userPosts = allPosts.Where(p => p.UserId == userId);
    
    return Ok(userPosts);
}
    }
}