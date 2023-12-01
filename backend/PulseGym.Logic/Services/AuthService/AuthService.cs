﻿using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<User> RegisterUserAsync(UserRegisterDTO userRegister, string role)
        {
            var newUser = new User
            {
                Email = userRegister.Email,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Birthday = userRegister.Birthday,
                UserName = userRegister.Email
            };

            var result = await _userManager.CreateAsync(newUser, userRegister.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Registration failed!");
            }

            var identityRole = new IdentityRole<Guid>(role);

            await _userManager.AddToRoleAsync(newUser, identityRole.Name);

            return newUser;
        }

        public async Task<TokensDTO> LoginUserAsync(UserLoginDTO user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                throw new Exception("User not found!");
            }

            var result = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            if (!result)
            {
                throw new Exception("Invalid password!");
            }

            var tokens = await _tokenService.GenerateTokensAsync(existingUser);

            return tokens;

        }

        public async Task LogoutAsync(Guid userId)
        {
            await _tokenService.DeleteTokens(userId);
        }


    }
}
