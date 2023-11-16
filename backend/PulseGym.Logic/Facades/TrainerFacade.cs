using Mapster;

using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Models;
using PulseGym.Entities.DTO.Trainer;
using PulseGym.Entities.DTO.User;
using PulseGym.Logic.Services.Auth;

namespace PulseGym.Logic.Facades
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

        public async Task<ICollection<TrainerListItem>> GetTrainersAsync()
        {
            var result = await _trainerRepository.GetAllTrainersAsync();

            return result.Adapt<TrainerListItem>();
        }
    }
}
