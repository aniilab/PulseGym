using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IClientFacade
    {
        Task<ICollection<ClientListItemDTO>> GetClientsAsync();

        Task<bool> CreateClientAsync(ClientCreateDTO newClient);
    }
}
