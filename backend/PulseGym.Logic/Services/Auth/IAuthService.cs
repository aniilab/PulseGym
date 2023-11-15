using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(UserRegister userRegister);
    }
}
