using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

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
                ?? throw new Exception($"Group Class with Id {id} not found!");

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
               ?? throw new Exception($"Group Class with Id {id} not found.");

            groupClass.Id = id;

            _context.GroupClasses.Update(foundGroupClass);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var foundActivity = await _context.Activities.FindAsync(id)
               ?? throw new Exception($"Activity with Id {id} not found.");

            _context.Activities.Remove(foundActivity);

            await _context.SaveChangesAsync();
        }
    }
}
