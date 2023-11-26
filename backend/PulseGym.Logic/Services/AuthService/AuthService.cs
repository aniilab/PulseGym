using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
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
                var identityRole = new IdentityRole<Guid>(role);

                await _userManager.AddToRoleAsync(newUser, identityRole.Name);
                await AddClaimsBasedOnRole(newUser, identityRole);

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
                    return await _tokenService.GenerateAsync(existingUser);
                }
            }

            return null;
        }

        public async Task Logout()
        {

        }

        private async Task AddClaimsBasedOnRole(User user, IdentityRole<Guid> role)
        {
            var roleClaims = await _roleManager.GetClaimsAsync(role);

            if (roleClaims != null)
            {
                await _userManager.AddClaimsAsync(user, roleClaims);
            }
        }

    }
}
