using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

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
                                               ?? throw new NotFoundException(nameof(Client), userId);

            return client;
        }

        public async Task CreateAsync(Guid id, Client client)
        {
            client.UserId = id;

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid clientId, Client client)
        {
            var foundClient = await _context.Clients.FindAsync(clientId)
                ?? throw new NotFoundException(nameof(Client), clientId);

            client.UserId = clientId;

            _context.Clients.Update(client);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid clientId)
        {
            var foundClient = await _context.Clients.FindAsync(clientId)
                ?? throw new NotFoundException(nameof(Client), clientId);

            _context.Clients.Remove(foundClient);

            await _context.SaveChangesAsync();
        }
    }
}
