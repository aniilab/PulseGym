using PulseGym.DAL.Models;

namespace PulseGym.Logic.Services.Token
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
