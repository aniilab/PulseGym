using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IGroupClassRepository
    {
        Task<ICollection<GroupClass>> GetAllAsync();

        Task<GroupClass> GetByIdAsync(Guid id);

        Task CreateAsync(GroupClass groupClass);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(Guid id, GroupClass groupClass);
    }
}
