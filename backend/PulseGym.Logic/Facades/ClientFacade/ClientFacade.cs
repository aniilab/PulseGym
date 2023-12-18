using Mapster;

using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.Enums;
using PulseGym.Entities.Exceptions;
using PulseGym.Entities.Infrastructure;
using PulseGym.Logic.DTO;
using PulseGym.Logic.Services;

namespace PulseGym.Logic.Facades
{
    public class ClientFacade : IClientFacade
    {
        private readonly IClientRepository _clientRepository;

        private readonly IAuthService _authService;

        private readonly UserManager<User> _userManager;

        private readonly ITrainerFacade _trainerFacade;
        public ClientFacade(IAuthService authService, IClientRepository clientRepository, UserManager<User> userManager, ITrainerFacade trainerFacade)
        {
            _authService = authService;
            _clientRepository = clientRepository;
            _userManager = userManager;
            _trainerFacade = trainerFacade;
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

        public async Task<ClientViewDTO> GetClientAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            return client.Adapt<ClientViewDTO>();
        }

        public async Task<bool> CreateClientAsync(ClientCreateDTO newClient)
        {
            var registered = await _authService.RegisterUserAsync(newClient.Adapt<UserRegisterDTO>(), RoleNames.Client);

            await _clientRepository.CreateAsync(registered.Id, newClient.Adapt<Client>());

            bool isCreated = await ExistsAsync(registered.Id);

            return isCreated;
        }

        public async Task UpdateClientAsync(Guid clientId, ClientUpdateDTO clientDTO)
        {
            var foundClient = await _clientRepository.GetByIdAsync(clientId);

            foundClient.User.FirstName = clientDTO.FirstName;
            foundClient.User.LastName = clientDTO.LastName;
            foundClient.User.Birthday = clientDTO.Birthday;
            foundClient.Goal = clientDTO.Goal;
            foundClient.InitialHeight = clientDTO.InitialHeight;
            foundClient.InitialWeight = clientDTO.InitialWeight;

            await _clientRepository.UpdateAsync(clientId, foundClient);
        }

        public async Task DeleteClientAsync(Guid clientId)
        {
            await _clientRepository.DeleteAsync(clientId);

            var user = await _userManager.FindByIdAsync(clientId.ToString())
                ?? throw new NotFoundException(nameof(User), clientId);

            await _userManager.DeleteAsync(user);
        }

        public async Task<bool> CheckClientAvailabilityAsync(Guid userId, DateTime dateTime)
        {
            var client = await _clientRepository.GetByIdAsync(userId);
            bool isAvailable = true;

            if (client.Workouts!.Any(w => w.WorkoutDateTime == dateTime
                    && (w.Status == WorkoutStatus.Planned || w.Status == WorkoutStatus.InProgress)))
            {
                isAvailable = false;
            }

            return isAvailable;
        }

        public async Task<ICollection<DateTime>> GetOccupiedDateTimeAsync(Guid userId)
        {
            var client = await _clientRepository.GetByIdAsync(userId);

            var occupiedDateTime = client.Workouts!.Where(w => w.Status == WorkoutStatus.Planned || w.Status == WorkoutStatus.InProgress)
                                                   .Select(w => w.WorkoutDateTime)
                                                   .ToList();

            return occupiedDateTime;
        }

        public async Task AddPersonalTrainer(Guid clientId, Guid trainerId)
        {
            if (!await _trainerFacade.ExistsAsync(trainerId))
            {
                throw new NotFoundException(nameof(Trainer), trainerId);
            }

            var client = await _clientRepository.GetByIdAsync(clientId);

            client.PersonalTrainerId = trainerId;

            await _clientRepository.UpdateAsync(clientId, client);
        }
    }
}
