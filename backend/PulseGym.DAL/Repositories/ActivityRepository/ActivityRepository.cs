using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly PulseGymDbContext _context;

        public ActivityRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Activity>> GetAllAsync()
        {
            return await _context.Activities.Include(a => a.Trainer)
                                                .ThenInclude(t => t.User)
                                            .Include(a => a.Clients)
                                            .ToListAsync();
        }
    }
}
