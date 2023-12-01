﻿using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
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
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO user)
        {
            var tokens = await _authService.LoginUserAsync(user);
            AddRefreshTokenToCookie(tokens.RefreshToken);

            return Ok(tokens.AccessToken);
        }

        [HttpPost("Refresh")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["x-refresh-token"];

            var tokens = await _tokenService.RefreshAsync(refreshToken!);
            AddRefreshTokenToCookie(tokens.RefreshToken);

            return Ok(tokens.AccessToken);
        }

        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult> RegisterAdmin(UserRegisterDTO admin)
        {
            var isRegistered = await _authService.RegisterUserAsync(admin, "admin");

            if (isRegistered != null)
            {
                return Ok("Created successfully!");
            }

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("Logout")]
        public async Task<ActionResult> Logout()
        {
            var id = HttpContext;
            var id1 = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id1, out Guid userId))
            {
                return Unauthorized();
            }

            await _authService.LogoutAsync(userId);

            return Ok();
        }

        private void AddRefreshTokenToCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true
            };
            Response.Cookies.Append("x-refresh-token", refreshToken, cookieOptions);
        }
    }
}
