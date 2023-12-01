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
                                         .Include(c => c.MembershipProgram)
                                         .Include(c => c.Workouts)
                                         .Include(c => c.WorkoutRequests)
                                         .Include(c => c.Activities)
                                         .Include(c => c.PersonalTrainer)
                                         .ToListAsync();
        }

        public async Task<bool> CreateAsync(Guid id, Client client)
        {
            client.UserId = id;

            await _context.Clients.AddAsync(client);
            var added = await _context.SaveChangesAsync();
            return added != 0;
        }
    }
}
