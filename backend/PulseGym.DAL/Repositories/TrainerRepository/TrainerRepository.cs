using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly PulseGymDbContext _context;

        public TrainerRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Trainer>> GetAllAsync()
        {
            return await _context.Trainers.Include(t => t.User)
                                    .Include(t => t.Workouts)
                                    .Include(t => t.WorkoutRequests)
                                    .Include(t => t.Activities)
                                    .ToListAsync();
        }

        public async Task<bool> CreateAsync(Guid id, Trainer trainer)
        {
            trainer.UserId = id;

            await _context.Trainers.AddAsync(trainer);
            var added = await _context.SaveChangesAsync();
            return added != 0;
        }
    }
}
