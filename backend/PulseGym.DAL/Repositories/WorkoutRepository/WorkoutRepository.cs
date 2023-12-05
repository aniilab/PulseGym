using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly PulseGymDbContext _context;

        public WorkoutRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Workout>> GetAllAsync()
        {
            return await _context.Workouts.Include(w => w.Trainer)
                                    .Include(w => w.Client)
                                    .ToListAsync();
        }
    }
}
