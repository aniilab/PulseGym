using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.Entities.DTO;
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

        public async Task<bool> RegisterUser(UserRegister userRegister)
        {
            var newUser = new User
            {
                UserName = userRegister.UserName,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Birthday = userRegister.Birthday,

            };

            var result = await _userManager.CreateAsync(newUser, userRegister.Password);

            return result.Succeeded;
        }

        public async Task<string?> LoginUser(UserLogin user)
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName);

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
    }
}
