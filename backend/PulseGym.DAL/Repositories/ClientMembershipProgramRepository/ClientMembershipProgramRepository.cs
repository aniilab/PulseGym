using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

namespace PulseGym.DAL.Repositories
{
    public class ClientMembershipProgramRepository : IClientMembershipProgramRepository
    {
        private readonly PulseGymDbContext _context;

        public ClientMembershipProgramRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ClientMembershipProgram>> GetAllAsync()
        {
            return await _context.ClientMembershipPrograms.Include(cmp => cmp.Client)
                                                              .ThenInclude(c => c.User)
                                                          .Include(cmp => cmp.MembershipProgram)
                                                          .ToListAsync();
        }

        public async Task<ICollection<ClientMembershipProgram>> GetByClientIdAsync(Guid clientId)
        {
            var clientProgram = await _context.ClientMembershipPrograms.Where(cmp => cmp.ClientId == clientId)
                                                                       .Include(cmp => cmp.Client)
                                                                           .ThenInclude(c => c!.User)
                                                                       .Include(cmp => cmp.MembershipProgram)
                                                                       .ToListAsync()
                ?? throw new NotFoundException(nameof(ClientMembershipProgram), nameof(Client), clientId.ToString());

            return clientProgram;
        }

        public async Task CreateAsync(ClientMembershipProgram program)
        {
            await _context.ClientMembershipPrograms.AddAsync(program);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, ClientMembershipProgram clientProgram)
        {
            var foundClientProgram = await _context.ClientMembershipPrograms.FindAsync(id)
              ?? throw new NotFoundException(nameof(ClientMembershipProgram), id);

            clientProgram.Id = id;

            _context.ClientMembershipPrograms.Update(clientProgram);

            await _context.SaveChangesAsync();
        }
    }
}
