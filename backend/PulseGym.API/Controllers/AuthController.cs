using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.DTO;
using PulseGym.Logic.Services.Auth;

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

        [HttpPost]
        public async Task<bool> Register(UserRegister user)
        {
            return await _authService.RegisterUser(user);
        }

        [HttpGet]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            return Ok();
        }
    }
}
