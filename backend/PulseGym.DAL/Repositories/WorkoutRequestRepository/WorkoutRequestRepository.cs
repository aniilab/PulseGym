using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public class WorkoutRequestRepository : IWorkoutRequestRepository
    {
        private readonly PulseGymDbContext _context;

        public WorkoutRequestRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<WorkoutRequest>> GetAllAsync()
        {
            return await _context.WorkoutRequests.Include(wr => wr.Trainer)
                                           .Include(wr => wr.Client)
                                           .Include(wr => wr.Workout)
                                           .ToListAsync();
        }
    }
}
