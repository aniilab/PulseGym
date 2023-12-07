using Mapster;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.DTO;
using PulseGym.Entities.Enums;

namespace PulseGym.Logic.Facades
{
    public class WorkoutFacade : IWorkoutFacade
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IWorkoutRequestRepository _workoutRequestRepository;

        private readonly IClientFacade _clientFacade;
        private readonly ITrainerFacade _trainerFacade;

        private readonly UserManager<User> _userManager;

        public WorkoutFacade(IWorkoutRequestRepository workoutRequestRepository,
                             IWorkoutRepository workoutRepository,
                             UserManager<User> userManager,
                             ITrainerFacade trainerFacade,
                             IClientFacade clientFacade)
        {
            _workoutRequestRepository = workoutRequestRepository;
            _workoutRepository = workoutRepository;
            _userManager = userManager;
            _trainerFacade = trainerFacade;
            _clientFacade = clientFacade;
        }

        public async Task<ICollection<WorkoutViewDTO>> GetWorkoutsAsync()
        {
            var workoutList = await _workoutRepository.GetAllAsync();

            return workoutList.Adapt<List<WorkoutViewDTO>>();
        }

        public async Task<ICollection<WorkoutRequestViewDTO>> GetWorkoutRequestsAsync()
        {
            var workoutRequestList = await _workoutRequestRepository.GetAllAsync();

            return workoutRequestList.Adapt<List<WorkoutRequestViewDTO>>();
        }

        public async Task CreateWorkoutRequestAsync(WorkoutRequestInDTO workoutRequestDTO)
        {
            if (!(await _trainerFacade.ExistsAsync(workoutRequestDTO.TrainerId)))
                throw new Exception($"Trainer with Id {workoutRequestDTO.TrainerId} not found!");

            bool participantsAvailable = await CheckParticipantsAvailabilityAsync(workoutRequestDTO.WorkoutDateTime,
                                                                                  workoutRequestDTO.ClientId,
                                                                                  workoutRequestDTO.TrainerId);

            if (!participantsAvailable) throw new Exception("Participants are not available at that time!");

            var entity = workoutRequestDTO.Adapt<WorkoutRequest>();

            await _workoutRequestRepository.CreateAsync(entity);
        }

        public async Task CreateWorkoutAsync(WorkoutInDTO workoutDTO)
        {
            if (!(await _trainerFacade.ExistsAsync(workoutDTO.TrainerId)))
                throw new Exception($"Trainer with Id {workoutDTO.TrainerId} not found!");

            if (!(await _clientFacade.ExistsAsync(workoutDTO.ClientId)))
                throw new Exception($"Client with Id {workoutDTO.ClientId} not found!");

            bool participantsAvailable = await CheckParticipantsAvailabilityAsync(workoutDTO.WorkoutDateTime,
                                                                                  workoutDTO.ClientId,
                                                                                  workoutDTO.TrainerId);

            if (!participantsAvailable) throw new Exception("Participants are not available at that time!");

            var entity = workoutDTO.Adapt<Workout>();

            await _workoutRepository.CreateAsync(entity);
        }

        public async Task AcceptWorkoutRequestAsync(Guid userId, Guid workoutRequestId)
        {
            var foundRequest = await _workoutRequestRepository.GetByIdAsync(workoutRequestId);

            if (userId != foundRequest.TrainerId) throw new Exception("Trainer Id mismatch!");

            var newWorkout = foundRequest.Adapt<Workout>();
            await _workoutRepository.CreateAsync(newWorkout);

            foundRequest.Status = WorkoutRequestStatus.AcceptedToSchedule;

            await _workoutRequestRepository.UpdateAsync(workoutRequestId, foundRequest);
        }

        public async Task DeclineWorkoutRequestAsync(Guid userId, Guid workoutRequestId)
        {
            var foundRequest = await _workoutRequestRepository.GetByIdAsync(workoutRequestId);

            if (foundRequest.Status != WorkoutRequestStatus.New && foundRequest.Status != WorkoutRequestStatus.AcceptedToSchedule)
            {
                throw new Exception("Workout request has already been declined.");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new Exception($"User with Id {userId} not found.");

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any()) throw new Exception("User has no role!");

            if (roles[0] == "admin")
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByAdmin;
            }
            else if (roles[0] == "client" && foundRequest.ClientId == userId)
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByClient;
            }
            else if (roles[0] == "trainer" && foundRequest.TrainerId == userId)
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByTrainer;
            }
            else throw new Exception("User Id mismatch!");

            await _workoutRequestRepository.UpdateAsync(workoutRequestId, foundRequest);
        }

        public async Task UpdateWorkoutAsync(Guid workoutId, WorkoutUpdateDTO workoutDTO, Guid userId)
        {
            var foundWorkout = await _workoutRepository.GetByIdAsync(workoutId);

            if (foundWorkout.TrainerId != userId) throw new Exception("Trainer Id mismatch");

            foundWorkout.Title = workoutDTO.Title;
            foundWorkout.WorkoutDateTime = workoutDTO.WorkoutDateTime;
            foundWorkout.ExerciseDescription = workoutDTO.ExerciseDescription;

            await _workoutRepository.UpdateAsync(workoutId, foundWorkout);
        }

        public async Task CancelWorkoutAsync(Guid userId, Guid workoutId)
        {
            var foundWorkout = await _workoutRepository.GetByIdAsync(workoutId);

            if (foundWorkout.Status == WorkoutStatus.CancelledByClient ||
                foundWorkout.Status == WorkoutStatus.CancelledByTrainer ||
                foundWorkout.Status == WorkoutStatus.CancelledByAdmin)
            {
                throw new Exception("Workout has already been cancelled.");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new Exception($"User with Id {userId} not found.");

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any()) throw new Exception("User has no role!");

            if (roles[0] == "admin")
            {
                foundWorkout.Status = WorkoutStatus.CancelledByAdmin;
            }
            else if (roles[0] == "client" && foundWorkout.ClientId == userId)
            {
                foundWorkout.Status = WorkoutStatus.CancelledByClient;
            }
            else if (roles[0] == "trainer" && foundWorkout.TrainerId == userId)
            {
                foundWorkout.Status = WorkoutStatus.CancelledByTrainer;
            }
            else throw new Exception("User Id mismatch!");

            await _workoutRepository.UpdateAsync(workoutId, foundWorkout);
        }

        private async Task<bool> CheckParticipantsAvailabilityAsync(DateTime dateTime, Guid clientId, Guid trainerId)
        {
            bool isTrainerAvailable = await _trainerFacade.CheckTrainerAvailabilityAsync(trainerId, dateTime);

            bool isClientAvailable = await _clientFacade.CheckClientAvailabilityAsync(clientId, dateTime);

            return isTrainerAvailable && isClientAvailable;
        }
    }
}
