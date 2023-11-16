using PulseGym.DAL.Models;
using PulseGym.Entities.DTO.User;

namespace PulseGym.Logic.Services.Auth
{
    public interface IAuthService
    {
        Task<string?> LoginUser(UserLogin user);

        Task<User?> RegisterUserAsync(UserRegister userRegister, string role);

        Task Logout();
    }
}
