using Mapster;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.Enums;
using PulseGym.Entities.Exceptions;
using PulseGym.Entities.Infrastructure;
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

        public WorkoutFacade(IWorkoutRequestRepository workoutRequestRepository,
                             IWorkoutRepository workoutRepository,
                             ITrainerFacade trainerFacade,
                             IClientFacade clientFacade,
                             IMembershipProgramFacade programFacade,
                             IClientRepository clientRepository)
        {
            _workoutRequestRepository = workoutRequestRepository;
            _workoutRepository = workoutRepository;
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

        public async Task<ICollection<WorkoutViewDTO>> GetUserWorkoutsAsync(string role, Guid userId)
        {
            var allWorkouts = (await _workoutRepository.GetAllAsync());

            List<Workout> userWorkouts;
            if (role == RoleNames.Client)
            {
                if (!await _clientFacade.ExistsAsync(userId))
                {
                    throw new NotFoundException(nameof(Client), userId);
                }

                userWorkouts = allWorkouts.Where(w => w.Clients.Any(c => c.UserId == userId)).ToList();
            }
            else if (role == RoleNames.Trainer)
            {
                if (!await _trainerFacade.ExistsAsync(userId))
                {
                    throw new NotFoundException(nameof(Trainer), userId);
                }

                userWorkouts = allWorkouts.Where(w => w.TrainerId == userId).ToList();
            }
            else
            {
                throw new BadInputException("Wrong role received!");
            }

            return userWorkouts.Adapt<List<WorkoutViewDTO>>();
        }

        public async Task CreateWorkoutAsync(WorkoutInDTO workoutDTO)
        {
            if (workoutDTO.WorkoutType == WorkoutType.Solo && (workoutDTO.TrainerId.HasValue || workoutDTO.ClientIds.Count > 1))
            {
                throw new BadInputException("Wrong participants for Solo Workout");
            }

            foreach (var clientId in workoutDTO.ClientIds)
            {
                if (!await _clientFacade.ExistsAsync(clientId))
                {
                    throw new NotFoundException(nameof(Client), clientId);
                }

                bool isAvailable = await _clientFacade.CheckClientAvailabilityAsync(clientId, workoutDTO.WorkoutDateTime);
                bool hasAccess = (await _programFacade.GetClientProgramsAsync(clientId)).Any(p => p.WorkoutType == workoutDTO.WorkoutType
                                                                                             && p.ExpirationDate > workoutDTO.WorkoutDateTime
                                                                                             && p.WorkoutRemainder > 0);

                if (!hasAccess)
                {
                    throw new InvalidMembershipProgramException($"Client with Id {clientId} cannot take part in this workout!");
                }
                else if (!isAvailable)
                {
                    throw new BadInputException($"Client with Id {clientId} is not available at this time");
                }
            }

            if (workoutDTO.TrainerId.HasValue)
            {
                if (!await _trainerFacade.ExistsAsync((Guid)workoutDTO.TrainerId))
                {
                    throw new NotFoundException(nameof(Trainer), (Guid)workoutDTO.TrainerId);
                }

                bool isAvailable = await _trainerFacade.CheckTrainerAvailabilityAsync((Guid)workoutDTO.TrainerId, workoutDTO.WorkoutDateTime) ? true
                    : throw new BadInputException($"Trainer with Id {workoutDTO.TrainerId} is not available at this time!");
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
                throw new BadInputException("Workout has already been cancelled.");
            }

            if (role == RoleNames.Admin)
            {
                foundWorkout.Status = WorkoutStatus.CancelledByAdmin;
            }
            else if (role == RoleNames.Client && foundWorkout.Clients.Any(c => c.UserId == userId))
            {
                if (foundWorkout.WorkoutType == WorkoutType.GroupClass)
                {
                    throw new UnauthorizedException("Client cannot cancel Group Workout");
                }

                foundWorkout.Status = WorkoutStatus.CancelledByClient;
            }
            else if (role == RoleNames.Trainer && foundWorkout.TrainerId == userId)
            {
                foundWorkout.Status = WorkoutStatus.CancelledByTrainer;
            }
            else throw new UnauthorizedException("User Ids mismatch!");

            await _workoutRepository.UpdateAsync(workoutId, foundWorkout);

            await _programFacade.ChangeWorkoutRemainderCount(userId, foundWorkout.WorkoutType, false);
        }

        public async Task RemoveClientFromWorkoutAsync(Guid workoutId, Guid clientId)
        {
            var workout = await _workoutRepository.GetByIdAsync(workoutId);

            if (workout.WorkoutType != WorkoutType.GroupClass)
            {
                throw new BadInputException("Unable to remove a Client from Solo or Personal Workout.");
            }

            var client = await _clientRepository.GetByIdAsync(clientId);

            if (!workout.Clients.Any(c => c == client))
            {
                throw new NotFoundException(nameof(Workout), nameof(Client), clientId.ToString());
            }

            workout.Clients.Remove(client);

            await _workoutRepository.UpdateAsync(workoutId, workout);

            await _programFacade.ChangeWorkoutRemainderCount(clientId, workout.WorkoutType, false);
        }

        public async Task UpdateWorkoutAsync(Guid workoutId, WorkoutUpdateDTO workoutDTO)
        {
            var foundWorkout = await _workoutRepository.GetByIdAsync(workoutId);

            foundWorkout.Notes = workoutDTO.Notes;
            foundWorkout.WorkoutDateTime = workoutDTO.WorkoutDateTime;

            if (workoutDTO.TrainerId.HasValue && await _trainerFacade.ExistsAsync((Guid)workoutDTO.TrainerId))
            {
                foundWorkout.TrainerId = workoutDTO.TrainerId;
            }

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
                throw new BadInputException("Unable to update cancelled Workout status");
            }

            await _workoutRepository.UpdateAsync(workoutId, workout);
        }

        public async Task<ICollection<WorkoutRequestViewDTO>> GetUserWorkoutRequestsAsync(string role, Guid userId)
        {
            var workoutRequestList = await _workoutRequestRepository.GetAllAsync();

            List<WorkoutRequest> userWorkoutRequests;
            if (role == RoleNames.Client)
            {
                userWorkoutRequests = workoutRequestList.Where(w => w.ClientId == userId).ToList();
            }
            else if (role == RoleNames.Trainer)
            {
                userWorkoutRequests = workoutRequestList.Where(w => w.TrainerId == userId).ToList();
            }
            else
            {
                throw new BadInputException("Wrong role received!");
            }

            return workoutRequestList.Adapt<List<WorkoutRequestViewDTO>>();
        }

        public async Task CreateWorkoutRequestAsync(WorkoutRequestInDTO workoutRequestDTO)
        {
            bool hasClientAccess = (await _programFacade.GetClientProgramsAsync(workoutRequestDTO.ClientId))
                                                        .Any(p => p.WorkoutType == WorkoutType.Personal
                                                             && p.ExpirationDate > workoutRequestDTO.WorkoutDateTime
                                                             && p.WorkoutRemainder > 0);

            if (!hasClientAccess)
            {
                throw new InvalidMembershipProgramException($"Client with Id {workoutRequestDTO.ClientId} cannot take part in this workout!");
            }

            bool isTrainerAvailable = await _trainerFacade.CheckTrainerAvailabilityAsync(workoutRequestDTO.TrainerId, workoutRequestDTO.WorkoutDateTime);
            bool isClientAvailable = await _clientFacade.CheckClientAvailabilityAsync(workoutRequestDTO.ClientId, workoutRequestDTO.WorkoutDateTime);

            if (!isClientAvailable || !isTrainerAvailable)
            {
                throw new BadInputException("Participants are not available at that time!");
            }

            var entity = workoutRequestDTO.Adapt<WorkoutRequest>();

            await _workoutRequestRepository.CreateAsync(entity);

            await _programFacade.ChangeWorkoutRemainderCount(workoutRequestDTO.ClientId, WorkoutType.Personal, true);
        }

        public async Task AcceptWorkoutRequestAsync(Guid userId, Guid workoutRequestId)
        {
            var foundRequest = await _workoutRequestRepository.GetByIdAsync(workoutRequestId);

            if (userId != foundRequest.TrainerId) throw new UnauthorizedException("Trainer Id mismatch!");

            if (foundRequest.Status != WorkoutRequestStatus.New)
            {
                throw new BadInputException("Workout request is not new.");
            }

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
                throw new BadInputException("Workout request has already been declined.");
            }

            if (role == RoleNames.Admin)
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByAdmin;
            }
            else if (role == RoleNames.Client && foundRequest.ClientId == userId)
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByClient;
            }
            else if (role == RoleNames.Trainer && foundRequest.TrainerId == userId)
            {
                foundRequest.Status = WorkoutRequestStatus.DeclinedByTrainer;
            }
            else throw new UnauthorizedException("User Id mismatch!");

            await _workoutRequestRepository.UpdateAsync(workoutRequestId, foundRequest);

            await _programFacade.ChangeWorkoutRemainderCount(foundRequest.ClientId, WorkoutType.Personal, false);
        }
    }
}
