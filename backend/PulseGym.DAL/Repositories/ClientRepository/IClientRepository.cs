using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IClientRepository
    {
        Task<ICollection<Client>> GetAllAsync();

        Task<Client> GetByIdAsync(Guid userId);

        Task CreateAsync(Guid id, Client client);

        Task UpdateAsync(Guid clientId, Client entity);

        Task DeleteAsync(Guid clientId);
    }
}
