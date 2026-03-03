using Microsoft.AspNetCore.Mvc;
using SkillStage.DTOs;
using SkillStage.Service.IService;

namespace SkillStage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            try
            {
                var result = await _authService.Register(registerDto);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                var token = await _authService.Login(loginDto);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}