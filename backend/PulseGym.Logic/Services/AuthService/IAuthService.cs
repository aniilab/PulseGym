using PulseGym.DAL.Models;
using PulseGym.Entities.DTO.UserDTO;

namespace PulseGym.Logic.Services.AuthService
{
    public interface IAuthService
    {
        Task<string?> LoginUser(UserLogin user);

        Task<User?> RegisterUserAsync(UserRegister userRegister, string role);

        Task Logout();
    }
}
