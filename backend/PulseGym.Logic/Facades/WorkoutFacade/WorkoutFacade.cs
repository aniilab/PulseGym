using Mapster;

using Microsoft.AspNetCore.Identity;

using PulseGym.DAL.Enums;
using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public class WorkoutFacade : IWorkoutFacade
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IWorkoutRequestRepository _workoutRequestRepository;
        private readonly IClientRepository _clientRepository;

        private readonly IMembershipProgramFacade _programFacade;
        private readonly IClientFacade _clientFacade;
        private readonly ITrainerFacade _trainerFacade;

        private readonly UserManager<User> _userManager;

        public WorkoutFacade(IWorkoutRequestRepository workoutRequestRepository,
                             IWorkoutRepository workoutRepository,
                             UserManager<User> userManager,
                             ITrainerFacade trainerFacade,
                             IClientFacade clientFacade,
                             IMembershipProgramFacade programFacade,
                             IClientRepository clientRepository)
        {
            _workoutRequestRepository = workoutRequestRepository;
            _workoutRepository = workoutRepository;
            _userManager = userManager;
            _trainerFacade = trainerFacade;
            _clientFacade = clientFacade;
            _programFacade = programFacade;
            _clientRepository = clientRepository;
        }

        public async Task<ICollection<WorkoutViewDTO>> GetGroupWorkoutsAsync(DateTime dateFrom, DateTime dateTo)
        {
            var allGroupWorkouts = (await _workoutRepository.GetAllAsync())
                .Where(w => w.WorkoutType == WorkoutType.GroupClass
                            && w.WorkoutDateTime >= dateFrom && w.WorkoutDateTime.AddHours(1) <= dateTo)
                .ToList();

            return allGroupWorkouts.Adapt<List<WorkoutViewDTO>>();

        }

        public async Task<ICollection<WorkoutViewDTO>> GetUserWorkoutsAsync(DateTime dateFrom, DateTime dateTo, string role, Guid userId)
        {
            var allWorkouts = (await _workoutRepository.GetAllAsync())
                .Where(w => w.WorkoutDateTime >= dateFrom && w.WorkoutDateTime.AddHours(1) <= dateTo);

            List<Workout> userWorkouts;
            if (role == "client")
            {
                userWorkouts = allWorkouts.Where(w => w.Clients.Any(c => c.UserId == userId)).ToList();
            }
            else if (role == "trainer")
            {
                userWorkouts = allWorkouts.Where(w => w.TrainerId == userId).ToList();
            }
            else
            {
                throw new Exception("Wrong role received!");
            }

            return userWorkouts.Adapt<List<WorkoutViewDTO>>();
        }

        public async Task CreateWorkoutAsync(WorkoutInDTO workoutDTO)
        {
            if (workoutDTO.WorkoutType == (int)WorkoutType.Solo && (workoutDTO.TrainerId.HasValue || workoutDTO.ClientIds.Count > 1))
            {
                throw new Exception("Wrong participants for Solo Workout");
            }

            foreach (var clientId in workoutDTO.ClientIds)
            {
                bool isAvailable = await _clientFacade.CheckClientAvailabilityAsync(clientId, workoutDTO.WorkoutDateTime);
                bool hasAccess = (await _programFacade.GetClientProgramsAsync(clientId)).Any(p => p.WorkoutType == ((WorkoutType)workoutDTO.WorkoutType).ToString()
                                                                                             && p.ExpirationDate > workoutDTO.WorkoutDateTime
                                                                                             && p.WorkoutRemainder > 0);

                if (!isAvailable || !hasAccess) throw new Exception("Client cannot take part in this workout!");
            }

            if (workoutDTO.TrainerId.HasValue)
            {
                bool isAvailable = await _trainerFacade.CheckTrainerAvailabilityAsync((Guid)workoutDTO.TrainerId, workoutDTO.WorkoutDateTime) ? true
                    : throw new Exception("Trainer is not available at this time!");
            }

            var entity = workoutDTO.Adapt<Workout>();

            entity.Clients = (await _clientRepository.GetAllAsync()).Where(c => workoutDTO.ClientIds.Any(id => id == c.UserId)).ToList();

            await _workoutRepository.CreateAsync(entity);

            foreach (var client in entity.Clients)
            {
                await _programFacade.ChangeWorkoutRemainderCount(client.UserId, entity.WorkoutType, true);
            }
        }

        public async Task CancelWorkoutAsync(Guid userId, string role, Guid workoutId)
        {
            var foundWorkout = await _workoutRepository.GetByIdAsync(workoutId);

            if (foundWorkout.Status == WorkoutStatus.CancelledByClient ||
                foundWorkout.Status == WorkoutStatus.CancelledByTrainer ||
                foundWorkout.Status == WorkoutStatus.CancelledByAdmin)
            {
                throw new Exception("Workout has already been cancelled.");
            }

            if (role == "admin")
            {
                foundWorkout.Status = WorkoutStatus.CancelledByAdmin;
            }
            else if (role == "client" && foundWorkout.Clients.Any(c => c.UserId == userId) && foundWorkout.WorkoutType != WorkoutType.GroupClass)
            {
                foundWorkout.Status = WorkoutStatus.CancelledByClient;
            }
            else if (role == "trainer" && foundWorkout.TrainerId == userId && foundWorkout.WorkoutType != WorkoutType.Solo)
            {
                foundWorkout.Status = WorkoutStatus.CancelledByTrainer;
            }
            else throw new Exception("User Id mismatch!");

            await _workoutRepository.UpdateAsync(workoutId, foundWorkout);

            await _programFacade.ChangeWorkoutRemainderCount(userId, foundWorkout.WorkoutType, false);
        }

        public async Task RemoveUserFromWorkoutAsync(Guid workoutId, Guid clientId)
        {
            var workout = await _workoutRepository.GetByIdAsync(workoutId);

            if (workout.WorkoutType != WorkoutType.GroupClass)
            {
                throw new Exception("Unable to remove a Client from Solo or Personal Workout.");
            }

            var client = await _clientRepository.GetByIdAsync(clientId);
            workout.Clients.Remove(client);

            await _workoutRepository.UpdateAsync(workoutId, workout);
        }

        public async Task UpdateWorkoutAsync(Guid workoutId, WorkoutUpdateDTO workoutDTO)
        {
            var foundWorkout = await _workoutRepository.GetByIdAsync(workoutId);

            foundWorkout.Notes = workoutDTO.Notes;
            foundWorkout.WorkoutDateTime = workoutDTO.WorkoutDateTime;
            foundWorkout.TrainerId = workoutDTO.TrainerId;
            foundWorkout.GroupClassId = workoutDTO.GroupClassId;

            await _workoutRepository.UpdateAsync(workoutId, foundWorkout);
        }

        public async Task UpdateWorkoutStatusAsync(Guid workoutId)
        {
            var workout = await _workoutRepository.GetByIdAsync(workoutId);

            if (workout.Status == WorkoutStatus.Planned)
            {
                workout.Status = WorkoutStatus.InProgress;
            }
            else if (workout.Status == WorkoutStatus.InProgress)
            {
                workout.Status = WorkoutStatus.Passed;
            }
            else
            {
                throw new Exception("Unable to update cancelled Workout status");
            }

            await _workoutRepository.UpdateAsync(workoutId, workout);
        }

        public async Task<ICollection<WorkoutRequestViewDTO>> GetUserWorkoutRequestsAsync(string role, Guid userId)
        {
            var workoutRequestList = await _workoutRequestRepository.GetAllAsync();

            List<WorkoutRequest> userWorkoutRequests;
            if (role == "client")
            {
                userWorkoutRequests = workoutRequestList.Where(w => w.ClientId == userId).ToList();
            }
            else if (role == "trainer")
            {
                userWorkoutRequests = workoutRequestList.Where(w => w.TrainerId == userId).ToList();
            }
            else
            {
                throw new Exception("Wrong role received!");
            }

            return workoutRequestList.Adapt<List<WorkoutRequestViewDTO>>();
        }

        public async Task CreateWorkoutRequestAsync(WorkoutRequestInDTO workoutRequestDTO)
        {
            bool isTrainerAvailable = await _trainerFacade.CheckTrainerAvailabilityAsync(workoutRequestDTO.TrainerId, workoutRequestDTO.WorkoutDateTime);
            bool isClientAvailable = await _clientFacade.CheckClientAvailabilityAsync(workoutRequestDTO.ClientId, workoutRequestDTO.WorkoutDateTime);

            if (!isClientAvailable || !isTrainerAvailable) throw new Exception("Participants are not available at that time!");

            var entity = workoutRequestDTO.Adapt<WorkoutRequest>();

            await _workoutRequestRepository.CreateAsync(entity);
        }

        public async Task AcceptWorkoutRequestAsync(Guid userId, Guid workoutRequestId)
        {
            var foundRequest = await _workoutRequestRepository.GetByIdAsync(workoutRequestId);

            if (foundRequest.Status != WorkoutRequestStatus.New)
            {
                throw new Exception("Workout request is not new.");
            }

            if (userId != foundRequest.TrainerId) throw new Exception("Trainer Id mismatch!");

            var newWorkout = foundRequest.Adapt<Workout>();
            await _workoutRepository.CreateAsync(newWorkout);

            foundRequest.Status = WorkoutRequestStatus.AcceptedToSchedule;

            await _workoutRequestRepository.UpdateAsync(workoutRequestId, foundRequest);
        }

        public async Task DeclineWorkoutRequestAsync(Guid userId, string role, Guid workoutRequestId)
        {
            var foundRequest = await _workoutRequestRepository.GetByIdAsync(workoutRequestId);

            if (foundRequest.Status != WorkoutRequestStatus.New && foundRequest.Status != WorkoutRequestStatus.AcceptedToSchedule)
            {
                throw new Exception("Workout request has already been declined.");
            }

            if (role == "admin")
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByAdmin;
            }
            else if (role == "client" && foundRequest.ClientId == userId)
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByClient;
            }
            else if (role == "trainer" && foundRequest.TrainerId == userId)
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByTrainer;
            }
            else throw new Exception("User Id mismatch!");

            await _workoutRequestRepository.UpdateAsync(workoutRequestId, foundRequest);
        }
    }
}
