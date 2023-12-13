using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IWorkoutFacade
    {
        Task<ICollection<WorkoutViewDTO>> GetGroupWorkoutsAsync(DateTime dateFrom, DateTime dateTo);

        Task<ICollection<WorkoutViewDTO>> GetUserWorkoutsAsync(DateTime dateFrom, DateTime dateTo, string role, Guid userId);

        Task CreateWorkoutAsync(WorkoutInDTO workoutInDTO);

        Task CancelWorkoutAsync(Guid userId, string role, Guid workoutId);

        Task RemoveUserFromWorkoutAsync(Guid workoutId, Guid clientId);

        Task UpdateWorkoutAsync(Guid workoutId, WorkoutUpdateDTO workout);

        Task<ICollection<WorkoutRequestViewDTO>> GetUserWorkoutRequestsAsync(string role, Guid userId);

        Task CreateWorkoutRequestAsync(WorkoutRequestInDTO workoutRequestInDTO);

        Task AcceptWorkoutRequestAsync(Guid userId, Guid workoutRequestId);

        Task DeclineWorkoutRequestAsync(Guid userId, string role, Guid workoutRequestId);
        Task UpdateWorkoutStatusAsync(Guid workoutId);
    }
}
