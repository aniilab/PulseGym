using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.Entities.DTO.User;
using PulseGym.Logic.Services.Token;

namespace PulseGym.Logic.Services.Auth
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

        public async Task<User?> RegisterUserAsync(UserRegister userRegister, string role)
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

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, role);
                await AddClaimsBasedOnRole(newUser, role);

                return newUser;
            }

            return null;
        }

        public async Task<string?> LoginUser(UserLogin user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (result)
                {
                    return _tokenService.Generate(existingUser);
                }
            }

            return null;
        }

        public async Task Logout()
        {

        }

        private async Task AddClaimsBasedOnRole(User user, string role)
        {
            switch (role)
            {
                case "admin":
                    await _userManager.AddClaimAsync(user, new Claim("canViewAll", "true"));
                    await _userManager.AddClaimAsync(user, new Claim("canEditAll", "true"));
                    await _userManager.AddClaimAsync(user, new Claim("canAddAll", "true"));
                    await _userManager.AddClaimAsync(user, new Claim("canDeleteAll", "true"));
                    break;

                case "trainer":
                    await _userManager.AddClaimAsync(user, new Claim("canViewClients", "true"));
                    await _userManager.AddClaimAsync(user, new Claim("canViewSchedule", "true"));
                    await _userManager.AddClaimAsync(user, new Claim("canViewRequests", "true"));
                    await _userManager.AddClaimAsync(user, new Claim("canViewWorkouts", "true"));
                    break;

                case "Client":
                    await _userManager.AddClaimAsync(user, new Claim("canViewOwnWorkouts", "true"));
                    await _userManager.AddClaimAsync(user, new Claim("canRequestWorkouts", "true"));
                    break;

                default:
                    break;
            }
        }

    }
}
