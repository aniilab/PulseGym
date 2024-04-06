using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

namespace PulseGym.DAL.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly PulseGymDbContext _context;

        public WorkoutRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<Workout> CreateAsync(Workout newWorkout)
        {
            var result = await _context.Workouts.AddAsync(newWorkout);

            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ICollection<Workout>> GetAllAsync()
        {
            return await _context.Workouts.Include(w => w.Trainer)
                                               .ThenInclude(t => t!.User)
                                          .Include(w => w.Clients)
                                               .ThenInclude(c => c.User)
                                          .Include(w => w.GroupClass)
                                          .ToListAsync();
        }

        public async Task<Workout> GetByIdAsync(Guid id)
        {
            var foundWorkout = await _context.Workouts.Include(w => w.Trainer)
                                                          .ThenInclude(t => t!.User)
                                                      .Include(w => w.Clients)
                                                          .ThenInclude(c => c.User)
                                                      .Include(w => w.GroupClass)
                                                      .FirstOrDefaultAsync(w => w.Id == id)
                 ?? throw new NotFoundException(nameof(Workout), id);

            return foundWorkout;
        }

        public async Task UpdateAsync(Guid id, Workout workout)
        {
            var foundWorkout = await _context.Workouts.FindAsync(id)
             ?? throw new NotFoundException(nameof(Workout), id);

            workout.Id = id;

            _context.Workouts.Update(workout);

            await _context.SaveChangesAsync();
        }
    }
}
