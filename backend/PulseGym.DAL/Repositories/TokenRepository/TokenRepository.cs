using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

namespace PulseGym.DAL.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly PulseGymDbContext _context;

        public TokenRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByTokenAsync(string refreshToken)
        {
            var userToken = await _context.UserTokens.FirstOrDefaultAsync(ut => ut.Value == refreshToken)
                ?? throw new NotFoundException("Refresh Token", "token", refreshToken);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userToken.UserId)
                ?? throw new NotFoundException(nameof(User), userToken.UserId);

            return user;
        }

        public async Task DeleteByUserIdAsync(Guid userId)
        {
            var userTokens = _context.UserTokens.Where(ut => ut.UserId == userId);

            _context.UserTokens.RemoveRange(userTokens);
            await _context.SaveChangesAsync();
        }
    }
}
