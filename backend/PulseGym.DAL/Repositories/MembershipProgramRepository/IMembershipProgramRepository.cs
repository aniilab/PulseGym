using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IMembershipProgramRepository
    {
        Task<ICollection<MembershipProgram>> GetAllAsync();
    }
}
