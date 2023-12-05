using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public class MembershipProgramRepository : IMembershipProgramRepository
    {
        private readonly PulseGymDbContext _context;

        public MembershipProgramRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<MembershipProgram>> GetAllAsync()
        {
            return await _context.MembershipPrograms.Include(mp => mp.Clients).ToListAsync();
        }
    }
}
