using PulseGym.DAL.Models;
using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Services
{
    public interface IAuthService
    {
        Task<string?> LoginUser(UserLogin user);

        Task<User?> RegisterUserAsync(UserRegister userRegister, string role);

        Task Logout();
    }
}
