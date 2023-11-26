using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IClientRepository
    {
        Task<ICollection<Client>> GetAllAsync();

        Task<bool> CreateAsync(Guid id, Client client);
    }
}
