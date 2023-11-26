using Mapster;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.DTO;
using PulseGym.Logic.Services;

namespace PulseGym.Logic.Facades
{
    public class TrainerFacade : ITrainerFacade
    {
        private readonly IAuthService _authService;

        ITrainerRepository _trainerRepository;

        public TrainerFacade(IAuthService authService, ITrainerRepository trainerRepository)
        {
            _authService = authService;
            _trainerRepository = trainerRepository;
        }

        public async Task<ICollection<TrainerListItem>> GetTrainersAsync()
        {
            var trainers = await _trainerRepository.GetAllAsync();

            return trainers.Adapt<List<TrainerListItem>>();
        }

        public async Task<bool> CreateTrainerAsync(TrainerCreate newTrainer)
        {
            var registered = await _authService.RegisterUserAsync(newTrainer.Adapt<UserRegister>(), "trainer");

            if (registered == null)
            {
                return false;
            }

            var result = await _trainerRepository.CreateAsync(registered.Id, newTrainer.Adapt<Trainer>());

            return result;
        }

    }
}
