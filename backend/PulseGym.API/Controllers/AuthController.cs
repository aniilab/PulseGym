﻿using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.Infrastructure;
using PulseGym.Logic.DTO;
using PulseGym.Logic.Services;

namespace PulseGym.API.Controllers;

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
    public async Task<ActionResult<TokensDTO>> Login(UserLoginRequestDTO user)
    {
        var tokens = await _authService.LoginUserAsync(user);

        return Ok(tokens);
    }

    [HttpPost("Refresh")]
    public async Task<ActionResult<TokensDTO>> RefreshToken(string refreshToken)
    {
        var tokens = await _tokenService.RefreshAsync(refreshToken!);

        return Ok(tokens);
    }

    [HttpPost("RegisterAdmin")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<ActionResult> RegisterAdmin(UserRegisterDTO admin)
    {
        var isRegistered = await _authService.RegisterUserAsync(admin, RoleNames.Admin);

        if (isRegistered != null)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete("Logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        var id = HttpContext.User.FindFirstValue("Id");

        if (!Guid.TryParse(id, out Guid userId))
        {
            return Unauthorized();
        }

        await _authService.LogoutAsync(userId);

        return Ok();
    }

    [HttpGet("User")]
    [Authorize]
    public async Task<ActionResult<UserLoginResponseDTO>> GetLoggedUser()
    {
        var id = HttpContext.User.FindFirstValue("Id");

        if (!Guid.TryParse(id, out Guid userId))
        {
            return Unauthorized();
        }

        var user = await _authService.GetUserAsync(userId);

        return Ok(user);
    }
}
