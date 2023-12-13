using PulseGym.DAL.Models;
using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Services
{
    public interface ITokenService
    {
        Task<TokensDTO> GenerateTokensAsync(User user);

        Task<TokensDTO> RefreshAsync(string refreshToken);

        Task DeleteTokens(Guid userId);

    }
}
