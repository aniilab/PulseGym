using Mapster;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.DTO;
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

        public async Task<ICollection<ClientListItem>> GetClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients.Adapt<List<ClientListItem>>();
        }

        public async Task<bool> CreateClientAsync(ClientCreate newClient)
        {
            var registered = await _authService.RegisterUserAsync(newClient.Adapt<UserRegister>(), "client");

            if (registered == null)
            {
                return false;
            }

            var result = await _clientRepository.CreateAsync(registered.Id, newClient.Adapt<Client>());

            return result;
        }

    }
}




