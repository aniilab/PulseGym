using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IClientMembershipProgramRepository
    {
        Task<ICollection<ClientMembershipProgram>> GetAllAsync();

        Task<ICollection<ClientMembershipProgram>> GetByClientIdAsync(Guid clientId);

        Task CreateAsync(ClientMembershipProgram program);

        Task UpdateAsync(Guid id, ClientMembershipProgram clientProgram);
    }
}
