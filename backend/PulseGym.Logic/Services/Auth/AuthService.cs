using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
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

        public async Task<bool> LoginUser(UserLogin user)
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName);
            if (existingUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(existingUser, user.Password);
        }
    }
}
