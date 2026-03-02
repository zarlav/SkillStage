using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SkillStage.Domain;
using SkillStage.Service;
using SkillStage.Service.IService;

namespace SkillStage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> VratiKorisnikaPoId(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<User>> DodajKorisnika(User user)
        {
            await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(VratiKorisnikaPoId), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AzurirajKorisnika(string id, User user)
        {
            var updated = await _userService.UpdateAsync(id, user);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ObrisiKorisnika(string id)
        {
            var deleted = await _userService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
