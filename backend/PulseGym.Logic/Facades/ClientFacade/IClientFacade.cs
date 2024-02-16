using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IClientFacade
    {
        Task<ICollection<ClientViewDTO>> GetClientsAsync();

        Task<ClientViewDTO> GetClientAsync(Guid id);

        Task<bool> CreateClientAsync(ClientCreateDTO newClient);

        Task UpdateClientAsync(Guid clientId, ClientUpdateDTO client);

        Task DeleteClientAsync(Guid clientId);

        Task<bool> ExistsAsync(Guid userId);

        Task<bool> CheckClientAvailabilityAsync(Guid clientId, DateTime dateTime);

        Task<ICollection<DateTime>> GetOccupiedDateTimeAsync(Guid userId);

        Task AddPersonalTrainer(Guid clientId, Guid? trainerId);
    }
}
