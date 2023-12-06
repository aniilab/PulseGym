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

        public async Task CreateAsync(Workout newWorkout)
        {
            await _context.Workouts.AddAsync(newWorkout);

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Workout>> GetAllAsync()
        {
            return await _context.Workouts.Include(w => w.Trainer)
                                               .ThenInclude(t => t.User)
                                           .Include(w => w.Client)
                                               .ThenInclude(c => c.User)
                                           .ToListAsync();
        }
    }
}
