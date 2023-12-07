using Mapster;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.DTO;
using PulseGym.Entities.Enums;
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

        public async Task<bool> ExistsAsync(Guid userId)
        {
            var trainers = await _trainerRepository.GetAllAsync();

            return trainers.Any(t => t.UserId == userId);
        }

        public async Task<ICollection<TrainerViewDTO>> GetTrainersAsync()
        {
            var trainers = await _trainerRepository.GetAllAsync();

            return trainers.Adapt<List<TrainerViewDTO>>();
        }

        public async Task<bool> CreateTrainerAsync(TrainerCreateDTO newTrainer)
        {
            var registered = await _authService.RegisterUserAsync(newTrainer.Adapt<UserRegisterDTO>(), "trainer");

            await _trainerRepository.CreateAsync(registered.Id, newTrainer.Adapt<Trainer>());

            bool isCreated = await ExistsAsync(registered.Id);

            return isCreated;
        }

        public async Task<bool> CheckTrainerAvailabilityAsync(Guid userId, DateTime dateTime)
        {
            var trainer = await _trainerRepository.GetByIdAsync(userId);
            bool isAvailable = true;

            if (trainer.Workouts.Any(w => w.WorkoutDateTime == dateTime
                  && (w.Status == WorkoutStatus.Planned || w.Status == WorkoutStatus.InProgress))
               || trainer.Activities.Any(a => a.DateTime == dateTime))
            {
                isAvailable = false;
            }

            return isAvailable;
        }

        public async Task<ICollection<DateTime>> GetOccupiedDateTimeAsync(Guid userId)
        {
            var trainer = await _trainerRepository.GetByIdAsync(userId);

            var occupiedDateTime = trainer.Workouts.Select(w => w.WorkoutDateTime).ToList();

            occupiedDateTime.AddRange(trainer.Activities.Select(a => a.DateTime).ToList());

            return occupiedDateTime;
        }
    }
}
