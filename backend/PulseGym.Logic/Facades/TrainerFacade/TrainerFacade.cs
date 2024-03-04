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
    public class TrainerFacade : ITrainerFacade
    {
        private readonly IAuthService _authService;

        private readonly ITrainerRepository _trainerRepository;

        private readonly UserManager<User> _userManager;

        public TrainerFacade(IAuthService authService, ITrainerRepository trainerRepository, UserManager<User> userManager)
        {
            _authService = authService;
            _trainerRepository = trainerRepository;
            _userManager = userManager;
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
            var registered = await _authService.RegisterUserAsync(newTrainer.Adapt<UserRegisterDTO>(), RoleNames.Trainer);

            await _trainerRepository.CreateAsync(registered.Id, newTrainer.Adapt<Trainer>());

            bool isCreated = await ExistsAsync(registered.Id);

            return isCreated;
        }

        public async Task<bool> CheckTrainerAvailabilityAsync(Guid userId, DateTime dateTime)
        {
            var trainer = await _trainerRepository.GetByIdAsync(userId);
            bool isAvailable = true;

            if (trainer.Workouts!.Any(w => w.WorkoutDateTime == dateTime
                  && (w.Status == WorkoutStatus.Planned || w.Status == WorkoutStatus.InProgress)))
            {
                isAvailable = false;
            }

            return isAvailable;
        }

        public async Task<ICollection<DateTime>> GetOccupiedDateTimeAsync(Guid userId)
        {
            var trainer = await _trainerRepository.GetByIdAsync(userId);

            var occupiedDateTime = trainer.Workouts!.Where(w => w.Status == WorkoutStatus.Planned || w.Status == WorkoutStatus.InProgress)
                                                    .Select(w => w.WorkoutDateTime)
                                                    .ToList();

            return occupiedDateTime;
        }

        public async Task DeleteTrainerAsync(Guid userId)
        {
            await _trainerRepository.DeleteAsync(userId);

            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new NotFoundException(nameof(User), userId);

            await _userManager.DeleteAsync(user);
        }
    }
}
