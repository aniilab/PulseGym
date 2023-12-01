using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

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
            var userToken = await _context.UserTokens.SingleAsync(ut => ut.Value == refreshToken);

            var user = await _context.Users.SingleAsync(u => u.Id == userToken.UserId);

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
