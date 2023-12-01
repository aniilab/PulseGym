using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IClientFacade
    {
        Task<ICollection<ClientViewDTO>> GetClientsAsync();

        Task<bool> CreateClientAsync(ClientCreateDTO newClient);
    }
}
