using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.DTO;
using PulseGym.Logic.Services;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            var token = await _authService.LoginUser(user);
            if (token != null)
            {
                return Ok(token);
            }

            return BadRequest();
        }
    }
}
