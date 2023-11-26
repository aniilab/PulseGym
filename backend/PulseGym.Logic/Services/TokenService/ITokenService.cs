using PulseGym.DAL.Models;

namespace PulseGym.Logic.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAsync(User user);
    }
}
