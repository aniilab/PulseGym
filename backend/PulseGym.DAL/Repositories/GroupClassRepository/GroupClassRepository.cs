using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

namespace PulseGym.DAL.Repositories
{
    public class GroupClassRepository : IGroupClassRepository
    {
        private readonly PulseGymDbContext _context;

        public GroupClassRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<GroupClass>> GetAllAsync()
        {
            return await _context.GroupClasses.ToListAsync();
        }

        public async Task<GroupClass> GetByIdAsync(Guid id)
        {
            var groupClass = await _context.GroupClasses.FirstOrDefaultAsync(gc => gc.Id == id)
                ?? throw new NotFoundException(nameof(GroupClass), id);

            return groupClass;
        }

        public async Task CreateAsync(GroupClass groupClass)
        {
            await _context.GroupClasses.AddAsync(groupClass);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, GroupClass groupClass)
        {
            var foundGroupClass = await _context.GroupClasses.FindAsync(id)
               ?? throw new NotFoundException(nameof(GroupClass), id);

            groupClass.Id = id;

            _context.GroupClasses.Update(foundGroupClass);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var foundGroupClass = await _context.GroupClasses.FindAsync(id)
               ?? throw new NotFoundException(nameof(GroupClass), id);

            _context.GroupClasses.Remove(foundGroupClass);

            await _context.SaveChangesAsync();
        }
    }
}
