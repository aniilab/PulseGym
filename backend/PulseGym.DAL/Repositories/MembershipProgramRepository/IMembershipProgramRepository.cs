using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IMembershipProgramRepository
    {
        Task<ICollection<MembershipProgram>> GetAllAsync();

        Task<MembershipProgram> GetByIdAsync(Guid id);

        Task CreateAsync(MembershipProgram program);

        Task UpdateAsync(Guid id, MembershipProgram program);

        Task DeleteAsync(Guid id);
    }
}
