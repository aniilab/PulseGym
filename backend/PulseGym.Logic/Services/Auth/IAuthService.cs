using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> LoginUser(UserLogin user);
        Task<bool> RegisterUser(UserRegister userRegister);
    }
}
