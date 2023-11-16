using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Services.Auth
{
    public interface IAuthService
    {
        Task<string> LoginUser(UserLogin user);

        Task<bool> RegisterUser(UserRegister userRegister);

        Task Logout();
    }
}
