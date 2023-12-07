using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IMembershipProgramRepository
    {
        Task CreateAsync(MembershipProgram entity);

        Task<ICollection<MembershipProgram>> GetAllAsync();
    }
}
