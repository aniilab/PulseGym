using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IClientFacade
    {
        Task<ICollection<ClientListItem>> GetClientsAsync();

        Task<bool> CreateClientAsync(ClientCreate newClient);
    }
}
