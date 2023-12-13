using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IClientFacade
    {
        Task<ICollection<ClientViewDTO>> GetClientsAsync();

        Task<bool> CreateClientAsync(ClientCreateDTO newClient);

        Task<bool> ExistsAsync(Guid userId);

        Task<bool> CheckClientAvailabilityAsync(Guid clientId, DateTime dateTime);

        Task<ICollection<DateTime>> GetOccupiedDateTimeAsync(Guid userId);
    }
}
