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

        private readonly UserManager<User> _userManager;

        public WorkoutFacade(IWorkoutRequestRepository workoutRequestRepository, IWorkoutRepository workoutRepository, UserManager<User> userManager)
        {
            _workoutRequestRepository = workoutRequestRepository;
            _workoutRepository = workoutRepository;
            _userManager = userManager;
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

        public async Task CreateWorkoutRequestAsync(WorkoutRequestInDTO workoutRequestInDTO)
        {
            var entity = workoutRequestInDTO.Adapt<WorkoutRequest>();

            await _workoutRequestRepository.CreateAsync(entity);
        }

        public async Task AcceptWorkoutRequestAsync(Guid userId, Guid workoutRequestId)
        {
            var foundRequest = await _workoutRequestRepository.GetByIdAsync(workoutRequestId);

            if (userId != foundRequest.TrainerId) throw new Exception("Trainer Ids mismatch!");

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
    }
}
