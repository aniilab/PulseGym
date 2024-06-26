﻿using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;
using PulseGym.Logic.DTO;

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
                throw new BadInputException($"Registration failed. {result.Errors.First().Description}");
            }

            var identityRole = new IdentityRole<Guid>(role);

            await _userManager.AddToRoleAsync(newUser, identityRole.Name!);

            return newUser;
        }

        public async Task<TokensDTO> LoginUserAsync(UserLoginRequestDTO user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email)
                ?? throw new NotFoundException(nameof(User), nameof(user.Email), user.Email);

            var result = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            if (!result)
            {
                throw new BadInputException("Invalid password!");
            }

            var tokens = await _tokenService.GenerateTokensAsync(existingUser);

            return tokens;

        }

        public async Task LogoutAsync(Guid userId)
        {
            await _tokenService.DeleteTokens(userId);
        }

        public async Task<UserLoginResponseDTO> GetUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new NotFoundException(nameof(User), userId);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                ?? throw new NotFoundException("User does not have a role");

            return new()
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ImageUrl = user.ImageUrl,
                Role = role
            };
        }
    }
}
