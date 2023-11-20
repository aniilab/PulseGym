using Mapster;

using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.DTO.TrainerDTO;
using PulseGym.Entities.DTO.UserDTO;
using PulseGym.Logic.Services.AuthService;

namespace PulseGym.Logic.Facades.TrainerFacade
{
    public class TrainerFacade : ITrainerFacade
    {
        private readonly IAuthService _authService;

        ITrainerRepository _trainerRepository;

        private readonly UserManager<User> _userManager;

        public TrainerFacade(IAuthService authService, ITrainerRepository trainerRepository, UserManager<User> userManager)
        {
            _authService = authService;
            _trainerRepository = trainerRepository;
            _userManager = userManager;
        }

        public async Task<ICollection<TrainerListItem>> GetTrainersAsync()
        {
            var trainers = await _trainerRepository.GetTrainersAsync();

            return trainers.Adapt<List<TrainerListItem>>();
        }

        public async Task<bool> CreateTrainerAsync(TrainerCreate newTrainer)
        {
            var registered = await _authService.RegisterUserAsync(newTrainer.Adapt<UserRegister>(), "trainer");

            if (registered == null)
            {
                return false;
            }

            var result = await _trainerRepository.CreateTrainerAsync(registered.Id, newTrainer.Adapt<Trainer>());

            return result;
        }

    }
}
