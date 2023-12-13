using Mapster;

using PulseGym.DAL.Enums;
using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Logic.DTO;
using PulseGym.Logic.Services;

namespace PulseGym.Logic.Facades
{
    public class ClientFacade : IClientFacade
    {
        private readonly IAuthService _authService;

        IClientRepository _clientRepository;

        public ClientFacade(IAuthService authService, IClientRepository clientRepository)
        {
            _authService = authService;
            _clientRepository = clientRepository;
        }

        public async Task<bool> ExistsAsync(Guid userId)
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients.Any(c => c.UserId == userId);
        }

        public async Task<ICollection<ClientViewDTO>> GetClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients.Adapt<List<ClientViewDTO>>();
        }

        public async Task<bool> CreateClientAsync(ClientCreateDTO newClient)
        {
            var registered = await _authService.RegisterUserAsync(newClient.Adapt<UserRegisterDTO>(), "client");

            await _clientRepository.CreateAsync(registered.Id, newClient.Adapt<Client>());

            bool isCreated = await ExistsAsync(registered.Id);

            return isCreated;
        }

        public async Task<bool> CheckClientAvailabilityAsync(Guid userId, DateTime dateTime)
        {
            var client = await _clientRepository.GetByIdAsync(userId);
            bool isAvailable = true;

            if (client.Workouts.Any(w => w.WorkoutDateTime == dateTime
                    && (w.Status == WorkoutStatus.Planned || w.Status == WorkoutStatus.InProgress)))
            {
                isAvailable = false;
            }

            return isAvailable;
        }

        public async Task<ICollection<DateTime>> GetOccupiedDateTimeAsync(Guid userId)
        {
            var client = await _clientRepository.GetByIdAsync(userId);

            var occupiedDateTime = client.Workouts.Where(w => w.Status == WorkoutStatus.Planned || w.Status == WorkoutStatus.InProgress)
                                                  .Select(w => w.WorkoutDateTime)
                                                  .ToList();

            return occupiedDateTime;
        }
    }
}




