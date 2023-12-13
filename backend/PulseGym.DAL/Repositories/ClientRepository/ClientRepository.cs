using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly PulseGymDbContext _context;

        public ClientRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Client>> GetAllAsync()
        {
            return await _context.Clients.Include(c => c.User)
                                         .Include(c => c.Workouts)
                                         .Include(c => c.WorkoutRequests)
                                         .Include(c => c.PersonalTrainer)
                                         .ToListAsync();
        }

        public async Task<Client> GetByIdAsync(Guid userId)
        {
            var client = await _context.Clients.Include(c => c.User)
                                               .Include(c => c.Workouts)
                                               .Include(c => c.WorkoutRequests)
                                               .Include(c => c.PersonalTrainer)
                                               .FirstOrDefaultAsync(t => t.UserId == userId)
                                               ?? throw new Exception($"Client with Id {userId} not found!");

            return client;
        }

        public async Task CreateAsync(Guid id, Client client)
        {
            client.UserId = id;

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

    }
}
