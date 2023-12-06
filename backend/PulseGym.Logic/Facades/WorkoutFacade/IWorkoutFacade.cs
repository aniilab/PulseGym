using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IWorkoutFacade
    {
        Task<ICollection<WorkoutViewDTO>> GetWorkoutsAsync();

        Task<ICollection<WorkoutRequestViewDTO>> GetWorkoutRequestsAsync();

        Task CreateWorkoutRequestAsync(WorkoutRequestInDTO workoutRequestInDTO);

        Task AcceptWorkoutRequestAsync(Guid userId, Guid workoutRequestId);

        Task DeclineWorkoutRequestAsync(Guid userId, Guid workoutRequestId);
    }
}
