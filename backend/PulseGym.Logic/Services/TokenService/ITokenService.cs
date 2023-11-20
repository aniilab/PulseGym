using PulseGym.DAL.Models;

namespace PulseGym.Logic.Services.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateAsync(User user);
    }
}
