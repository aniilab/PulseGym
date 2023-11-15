﻿using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegister user)
        {
            if (await _authService.RegisterUser(user))
            {
                return Ok("Successfully registered!");
            }

            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (await _authService.LoginUser(user))
            {
                return Ok("Done");
            }

            return BadRequest();
        }
    }
}
