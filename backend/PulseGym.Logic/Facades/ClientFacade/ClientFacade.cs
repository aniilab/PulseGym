using Mapster;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.DTO;
using PulseGym.Entities.Enums;
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
                    && (w.Status == WorkoutStatus.Planned || w.Status == WorkoutStatus.InProgress))
               || client.Activities.Any(a => a.DateTime == dateTime))
            {
                isAvailable = false;
            }

            return isAvailable;
        }

        public async Task<ICollection<DateTime>> GetOccupiedDateTimeAsync(Guid userId)
        {
            var client = await _clientRepository.GetByIdAsync(userId);

            var occupiedDateTime = client.Workouts.Select(w => w.WorkoutDateTime).ToList();

            occupiedDateTime.AddRange(client.Activities.Select(a => a.DateTime).ToList());

            return occupiedDateTime;
        }
    }
}




