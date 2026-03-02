using SkillStage.Domain;

namespace SkillStage.Service.IService
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(string id);
        Task CreateAsync(User user);
        Task<bool> UpdateAsync(string id, User user);
        Task<bool> DeleteAsync(string id);
    }
}
