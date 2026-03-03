using SkillStage.DTOs;

namespace SkillStage.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDTO registerDto);
        Task<string> Login(LoginDTO loginDto);
    }
}