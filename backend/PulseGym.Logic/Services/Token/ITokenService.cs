using PulseGym.DAL.Models;

namespace PulseGym.Logic.Services.Token
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
