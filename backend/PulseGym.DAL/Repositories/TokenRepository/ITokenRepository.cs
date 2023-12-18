using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface ITokenRepository
    {
        Task<User> GetUserByTokenAsync(string refreshToken);

        Task DeleteByUserIdAsync(Guid userId);
    }
}
