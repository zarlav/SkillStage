using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SkillStage.Domain;
using SkillStage.DTOs;
using SkillStage.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SkillStage.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration; 

        public AuthService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<string> Register(RegisterDTO registerDto)
        {
            var user = new User
            {
                Name = registerDto.Name,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Password = registerDto.Password 
            };

            await _userService.CreateAsync(user);
            return "Registracija uspešna";
        }

        public async Task<string> Login(LoginDTO loginDto)
        {
            var user = await _userService.GetByUsernameAsync(loginDto.UserName);

            if (user == null || user.Password != loginDto.Password)
                throw new Exception("Pogrešno korisničko ime ili lozinka");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id!),
                    new Claim(ClaimTypes.Name, user.UserName!)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}